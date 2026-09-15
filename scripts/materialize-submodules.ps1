#Requires -Version 5.1
<#
.SYNOPSIS
  Materializa skills, agentes, prompts y reglas del consumidor SDAF como symlinks relativos.

.DESCRIPTION
  Fuente de verdad de los enlaces pack (sdaf-stack-dotnet) + core (sdaf-core) + superficie Cursor.
  Idempotente: no falla si el enlace ya apunta al destino correcto.
  No crea junctions (mklink /J); solo symlinks portables (Git mode 120000).
  No hace git add del enlace Windows: el índice usa target relativo con '/' para no ensuciar status.

.PARAMETER WhatIf
  Lista enlaces que se crearían o reemplazarían sin modificar el disco.

.PARAMETER Force
  Elimina copias o enlaces incorrectos antes de crear el symlink.

.EXAMPLE
  .\scripts\materialize-submodules.ps1 -WhatIf

.EXAMPLE
  .\scripts\materialize-submodules.ps1 -Force

.NOTES
  Prerequisitos: submodules inicializados; git config core.symlinks true;
  en Windows, Modo desarrollador o shell elevado para crear symlinks.
  Ver docs/materializacion-submodules.md
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [switch]$Force
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path

function Test-SubmodulePresent {
    param([string]$RelativePath)
    $full = Join-Path $RepoRoot $RelativePath
    if (-not (Test-Path $full)) {
        throw "Falta el submodule '$RelativePath'. Ejecuta: git submodule update --init --recursive"
    }
}

function Get-NormalizedTarget {
    param([string]$TargetRelative)
    return (($TargetRelative -replace '\\', '/').Trim().TrimEnd('/'))
}

function Test-IsWindowsPathRooted {
    # Path.IsPathRooted lanza ArgumentException con caracteres inválidos; no usarla.
    param([string]$Path)
    if ([string]::IsNullOrWhiteSpace($Path)) { return $false }
    $p = ($Path.Trim().Trim('"') -replace '/', '\')
    if ($p.Length -ge 3 -and $p[1] -eq [char]':') { return $true }
    if ($p.StartsWith('\\')) { return $true }
    return $false
}

function Get-LinkTargetRaw {
    param([string]$LinkPath)
    if (-not (Test-Path -LiteralPath $LinkPath)) { return $null }
    $item = Get-Item -LiteralPath $LinkPath -Force
    if ($item.Attributes -band [IO.FileAttributes]::ReparsePoint) {
        $t = $item.Target
        if ($null -eq $t) { return $null }
        if ($t -is [System.Array]) {
            if ($t.Length -eq 0) { return $null }
            return ([string]$t[0]).Trim()
        }
        return ([string]$t).Trim()
    }
    # Placeholder Git sin symlink OS: archivo de texto con el target relativo
    if (-not $item.PSIsContainer -and $item.Length -gt 0 -and $item.Length -lt 1024) {
        $content = $null
        try {
            $content = (Get-Content -LiteralPath $LinkPath -Raw -Encoding utf8 -ErrorAction Stop)
        }
        catch {
            return $null
        }
        if ($null -eq $content) { return $null }
        $line = ($content -split "`r?`n", 2)[0].Trim()
        if ($line -match '^\.\./' -or $line -match '^\./') {
            return $line
        }
    }
    return $null
}

function Test-LinkPointsToExpected {
    param(
        [string]$LinkPath,
        [string]$LinkDir,
        [string]$ExpectedRelative
    )
    $raw = Get-LinkTargetRaw -LinkPath $LinkPath
    if ([string]::IsNullOrWhiteSpace($raw)) { return $false }

    $normalizedRaw = Get-NormalizedTarget -TargetRelative $raw
    $normalizedExpected = Get-NormalizedTarget -TargetRelative $ExpectedRelative
    if ($normalizedRaw -eq $normalizedExpected) { return $true }

    try {
        $expectedFull = [System.IO.Path]::GetFullPath((Join-Path $LinkDir ($ExpectedRelative -replace '/', '\')))
    }
    catch {
        return $false
    }

    $candidate = if (Test-IsWindowsPathRooted -Path $raw) {
        ($raw -replace '/', '\')
    }
    else {
        Join-Path $LinkDir ($raw -replace '/', '\')
    }
    try {
        $actualFull = [System.IO.Path]::GetFullPath($candidate)
    }
    catch {
        return $false
    }
    return $actualFull -eq $expectedFull
}

function Get-IndexSymlinkTarget {
    param([string]$LinkRelative)
    $line = (git -C $RepoRoot ls-files -s -- $LinkRelative 2>$null | Select-Object -First 1)
    if (-not $line) { return $null }
    # mode SP hash SP stage TAB path  (p.ej. 120000 <hash> 0<TAB>path)
    if ($line -notmatch '^120000\s+([0-9a-f]{40,64})\s+') {
        return $null
    }
    $hash = $Matches[1]
    $blob = (git -C $RepoRoot cat-file -p $hash 2>$null)
    if (-not $blob) { return $null }
    return Get-NormalizedTarget -TargetRelative ([string]$blob).Trim()
}

function Remove-LinkOrCopy {
    param([string]$LinkPath)
    if (-not (Test-Path -LiteralPath $LinkPath)) { return }
    $item = Get-Item -LiteralPath $LinkPath -Force
    if ($item.Attributes -band [IO.FileAttributes]::ReparsePoint) {
        Remove-Item -LiteralPath $LinkPath -Force
    }
    elseif ($item.PSIsContainer) {
        Remove-Item -LiteralPath $LinkPath -Recurse -Force
    }
    else {
        Remove-Item -LiteralPath $LinkPath -Force
    }
}

function Set-GitSymlinkIndex {
    <#
      Asegura mode 120000 con target relativo en '/'.
      No hace git add (evita capturar '\' de Windows) ni checkout-index
      (el caller decide si materializa el working tree).
    #>
    param(
        [string]$LinkRelative,
        [string]$TargetRelative
    )
    $normalizedTarget = Get-NormalizedTarget -TargetRelative $TargetRelative
    $current = Get-IndexSymlinkTarget -LinkRelative $LinkRelative
    if ($current -eq $normalizedTarget) {
        return $false
    }
    $hash = ($normalizedTarget | git -C $RepoRoot hash-object -w --stdin).Trim()
    if (-not $hash) {
        throw "git hash-object falló para '$LinkRelative'"
    }
    git -C $RepoRoot update-index --add --cacheinfo "120000,$hash,$LinkRelative" | Out-Null
    return $true
}

function New-RelativeSymlink {
    param(
        [string]$LinkRelative,
        [string]$TargetRelative
    )

    $linkPath = Join-Path $RepoRoot $LinkRelative
    $linkDir = Split-Path $linkPath -Parent
    $normalizedExpected = Get-NormalizedTarget -TargetRelative $TargetRelative
    try {
        $targetPath = [System.IO.Path]::GetFullPath((Join-Path $linkDir ($TargetRelative -replace '/', '\')))
    }
    catch {
        throw "Destino inválido para '$LinkRelative': $TargetRelative ($($_.Exception.Message))"
    }

    if (-not (Test-Path -LiteralPath $targetPath)) {
        throw "Destino inexistente para '$LinkRelative': $TargetRelative"
    }

    $osOk = Test-LinkPointsToExpected -LinkPath $linkPath -LinkDir $linkDir -ExpectedRelative $TargetRelative
    $indexTarget = Get-IndexSymlinkTarget -LinkRelative $LinkRelative
    $indexOk = ($indexTarget -eq $normalizedExpected)

    if ($osOk -and $indexOk) {
        Write-Verbose "OK: $LinkRelative"
        return 'skipped'
    }

    if ($osOk -and -not $indexOk) {
        # Disco correcto; solo alinear índice a '/' sin tocar el working tree.
        if ($PSCmdlet.ShouldProcess($LinkRelative, "alinear índice Git 120000 -> $normalizedExpected")) {
            [void](Set-GitSymlinkIndex -LinkRelative $LinkRelative -TargetRelative $TargetRelative)
            return 'created'
        }
        return 'whatif'
    }

    if ((Test-Path -LiteralPath $linkPath) -and -not $Force) {
        $raw = Get-LinkTargetRaw -LinkPath $linkPath
        if ($null -ne $raw) {
            throw "Enlace incorrecto en '$LinkRelative' (actual: $raw). Usa -Force."
        }
        throw "Existe copia en '$LinkRelative'. Usa -Force para reemplazar por symlink."
    }

    if ($PSCmdlet.ShouldProcess($LinkRelative, "symlink -> $normalizedExpected")) {
        if (-not (Test-Path -LiteralPath $linkDir)) {
            New-Item -ItemType Directory -Path $linkDir -Force | Out-Null
        }
        # Solo filesystem: no git rm (ensucia el índice sin necesidad).
        Remove-LinkOrCopy -LinkPath $linkPath
        # Índice primero con '/' (blob canónico); evita git add de targets '\'.
        [void](Set-GitSymlinkIndex -LinkRelative $LinkRelative -TargetRelative $TargetRelative)

        $prevEap = $ErrorActionPreference
        $ErrorActionPreference = 'Continue'
        git -C $RepoRoot checkout-index -f -- $LinkRelative 2>&1 | Out-Null
        $checkoutOk = ($LASTEXITCODE -eq 0)
        $ErrorActionPreference = $prevEap

        if ($checkoutOk -and (Test-LinkPointsToExpected -LinkPath $linkPath -LinkDir $linkDir -ExpectedRelative $TargetRelative)) {
            return 'created'
        }

        # Fallback OS: crear desde $linkDir (New-Item resuelve -Target contra el CWD).
        try {
            Remove-LinkOrCopy -LinkPath $linkPath
            $linkName = Split-Path $linkPath -Leaf
            Push-Location -LiteralPath $linkDir
            try {
                try {
                    New-Item -ItemType SymbolicLink -Path $linkName -Target $normalizedExpected -ErrorAction Stop | Out-Null
                }
                catch {
                    $targetBackslash = ($normalizedExpected -replace '/', '\')
                    New-Item -ItemType SymbolicLink -Path $linkName -Target $targetBackslash -ErrorAction Stop | Out-Null
                }
            }
            finally {
                Pop-Location
            }
            if (-not (Test-LinkPointsToExpected -LinkPath $linkPath -LinkDir $linkDir -ExpectedRelative $TargetRelative)) {
                throw "El symlink OS no apunta al destino esperado"
            }
        }
        catch {
            Write-Warning "Symlink OS no disponible para '$LinkRelative' ($($_.Exception.Message)). Índice Git 120000 alineado; activa Modo desarrollador si el contenido no se abre."
        }
        return 'created'
    }
    return 'whatif'
}

# Manifesto único: ruta en consumidor -> destino relativo desde el directorio padre del enlace
$Manifest = @(
    # Pack — skills
    @{ Link = 'skills/csharp-adr006-slice'; Target = '../sdaf-stack-dotnet/skills/csharp-adr006-slice' }
    @{ Link = 'skills/blazor-bff-slice'; Target = '../sdaf-stack-dotnet/skills/blazor-bff-slice' }
    @{ Link = 'skills/aspire-local-run'; Target = '../sdaf-stack-dotnet/skills/aspire-local-run' }

    # Pack — agents
    @{ Link = 'agents/domain-application-agent.md'; Target = '../sdaf-stack-dotnet/agents/domain-application-agent.md' }
    @{ Link = 'agents/frontend-agent.md'; Target = '../sdaf-stack-dotnet/agents/frontend-agent.md' }
    @{ Link = 'agents/infrastructure-agent.md'; Target = '../sdaf-stack-dotnet/agents/infrastructure-agent.md' }

    # Pack — prompts
    @{ Link = 'prompts/agents/domain-application-agent.md'; Target = '../../sdaf-stack-dotnet/prompts/agents/domain-application-agent.md' }
    @{ Link = 'prompts/agents/frontend-agent.md'; Target = '../../sdaf-stack-dotnet/prompts/agents/frontend-agent.md' }
    @{ Link = 'prompts/agents/infrastructure-agent.md'; Target = '../../sdaf-stack-dotnet/prompts/agents/infrastructure-agent.md' }

    # Pack — regla Cursor
    @{ Link = '.cursor/rules/coding-standards-csharp.mdc'; Target = '../../sdaf-stack-dotnet/.cursor/rules/coding-standards-csharp.mdc' }

    # Core — skills
    @{ Link = 'skills/sdaf-gate0'; Target = '../sdaf-core/skills/sdaf-gate0' }
    @{ Link = 'skills/sdaf-bootstrap'; Target = '../sdaf-core/skills/sdaf-bootstrap' }
    @{ Link = 'skills/sdaf-upgrade'; Target = '../sdaf-core/skills/sdaf-upgrade' }
    @{ Link = 'skills/sdaf-agent-router'; Target = '../sdaf-core/skills/sdaf-agent-router' }
    @{ Link = 'skills/sdaf-worklog-handoff'; Target = '../sdaf-core/skills/sdaf-worklog-handoff' }
    @{ Link = 'skills/adr-propose'; Target = '../sdaf-core/skills/adr-propose' }
    @{ Link = 'skills/spec-draft-pbi'; Target = '../sdaf-core/skills/spec-draft-pbi' }

    # Core — agents
    @{ Link = 'agents/specification-agent.md'; Target = '../sdaf-core/agents/specification-agent.md' }
    @{ Link = 'agents/architecture-agent.md'; Target = '../sdaf-core/agents/architecture-agent.md' }
    @{ Link = 'agents/testing-review-agent.md'; Target = '../sdaf-core/agents/testing-review-agent.md' }
    @{ Link = 'agents/product-agent.md'; Target = '../sdaf-core/agents/product-agent.md' }
    @{ Link = 'agents/domain-agent.md'; Target = '../sdaf-core/agents/domain-agent.md' }
    @{ Link = 'agents/application-agent.md'; Target = '../sdaf-core/agents/application-agent.md' }
    @{ Link = 'agents/devops-agent.md'; Target = '../sdaf-core/agents/devops-agent.md' }
    @{ Link = 'agents/review-agent.md'; Target = '../sdaf-core/agents/review-agent.md' }
    @{ Link = 'agents/testing-agent.md'; Target = '../sdaf-core/agents/testing-agent.md' }

    # Core — prompts
    @{ Link = 'prompts/agents/specification-agent.md'; Target = '../../sdaf-core/prompts/agents/specification-agent.md' }
    @{ Link = 'prompts/agents/architecture-agent.md'; Target = '../../sdaf-core/prompts/agents/architecture-agent.md' }
    @{ Link = 'prompts/agents/testing-review-agent.md'; Target = '../../sdaf-core/prompts/agents/testing-review-agent.md' }
    @{ Link = 'prompts/agents/product-agent.md'; Target = '../../sdaf-core/prompts/agents/product-agent.md' }
    @{ Link = 'prompts/agents/domain-agent.md'; Target = '../../sdaf-core/prompts/agents/domain-agent.md' }
    @{ Link = 'prompts/agents/application-agent.md'; Target = '../../sdaf-core/prompts/agents/application-agent.md' }
    @{ Link = 'prompts/agents/devops-agent.md'; Target = '../../sdaf-core/prompts/agents/devops-agent.md' }
    @{ Link = 'prompts/agents/review-agent.md'; Target = '../../sdaf-core/prompts/agents/review-agent.md' }
    @{ Link = 'prompts/agents/testing-agent.md'; Target = '../../sdaf-core/prompts/agents/testing-agent.md' }

    # Core — regla Cursor
    @{ Link = '.cursor/rules/idioma-castellano.mdc'; Target = '../../sdaf-core/.cursor/rules/idioma-castellano.mdc' }
)

Push-Location $RepoRoot
try {
    Test-SubmodulePresent 'sdaf-core'
    Test-SubmodulePresent 'sdaf-stack-dotnet'

    $stats = @{ created = 0; skipped = 0; whatif = 0 }

    foreach ($entry in $Manifest) {
        $result = New-RelativeSymlink -LinkRelative $entry.Link -TargetRelative $entry.Target
        $stats[$result]++
    }

    # Cursor: .cursor/skills/<id> -> submodule (misma fuente que skills/<id>)
    $skillLinks = $Manifest | Where-Object { $_.Link -like 'skills/*' -and $_.Link -notmatch '\.' }
    foreach ($entry in $skillLinks) {
        $id = Split-Path $entry.Link -Leaf
        $cursorTarget = if ($entry.Target -like '../*') { '../' + $entry.Target } else { $entry.Target }
        $result = New-RelativeSymlink -LinkRelative ".cursor/skills/$id" -TargetRelative $cursorTarget
        $stats[$result]++
    }

    Write-Host ''
    Write-Host "Resumen: creados=$($stats.created) omitidos=$($stats.skipped) whatif=$($stats.whatif)"
    Write-Host 'Siguiente: git status (debe quedar limpio si solo rematerializaste enlaces ya correctos).'
    Write-Host 'Editar contenido en sdaf-core/ o sdaf-stack-dotnet/, no en la ruta enlazada del consumidor.'
    exit 0
}
finally {
    Pop-Location
}
