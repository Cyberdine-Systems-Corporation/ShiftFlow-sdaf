# Materialización de submodules (pack + core)

Este consumidor no duplica skills, agentes, prompts ni reglas del método o del pack: los **enlaza** con symlinks relativos (Git mode `120000`).

## Cuándo ejecutar

- Tras clonar con submodules (`git clone --recurse-submodules` o `git submodule update --init --recursive`).
- Tras adoptar o actualizar el pin de `sdaf-core` o `sdaf-stack-dotnet`.
- Tras cambiar de rama si los enlaces no están presentes.

## Prerequisitos

| Requisito | Comando / nota |
|-----------|----------------|
| Submodules | `git submodule update --init --recursive` |
| Git symlinks | `git config core.symlinks true` |
| Windows | Modo desarrollador **o** PowerShell elevado (permiso para crear symlinks) |
| Linux/macOS | Sin pasos extra |

## Script (fuente de verdad del manifesto)

Desde la raíz del repo:

```powershell
# Vista previa
.\scripts\materialize-submodules.ps1 -WhatIf

# Aplicar (reemplaza copias antiguas)
.\scripts\materialize-submodules.ps1 -Force
```

En Unix / CI:

```bash
chmod +x scripts/materialize-submodules.sh
./scripts/materialize-submodules.sh --what-if
./scripts/materialize-submodules.sh --force
```

El manifesto de pares `(enlace, destino)` vive **solo** en los scripts; este documento describe el procedimiento.

## Qué se materializa

| Origen | Rutas en el consumidor |
|--------|-------------------------|
| `sdaf-stack-dotnet` | `skills/csharp-*`, `blazor-*`, `aspire-*`; agentes/prompts pack; `.cursor/rules/coding-standards-csharp.mdc` |
| `sdaf-core` | `skills/sdaf-*`, `adr-propose`, `spec-draft-pbi`; agentes/prompts núcleo; `.cursor/rules/idioma-castellano.mdc` |
| Cursor | `.cursor/skills/<id>` → submodule (misma fuente que `skills/<id>`) |

Archivos **reales** del consumidor (no symlinks): `skills/README.md`, `AGENTS.md`, handbook de producto, specs, etc.

## Verificación

```powershell
git ls-files -s skills/csharp-adr006-slice
# Debe mostrar mode 120000

git status
```

Abrir una skill por `skills/<id>/SKILL.md` y por `.cursor/skills/<id>/SKILL.md`; debe ser el mismo contenido.

## Upgrade de pack o core

1. Actualizar submodule al nuevo tag.
2. Volver a ejecutar el script (`-Force` / `--force` si cambió la estructura de rutas).
3. Revisar `AGENTS.md` y worklogs si cambió el inventario de agentes.

## Edición de contenido

**No** editar skills/agentes/prompts/reglas a través de las rutas enlazadas en el consumidor para cambios upstream. Editar en `sdaf-core/` o `sdaf-stack-dotnet/` y commitear en esos repos; el consumidor solo mantiene el pin del submodule.

## Troubleshooting

| Síntoma | Acción |
|---------|--------|
| `New-Item : Se necesitan privilegios de administrador` | Activar Modo desarrollador en Windows o ejecutar PowerShell como admin |
| Enlaces aparecen como archivos de texto tras clone | `git config core.symlinks true` y volver a checkout |
| `Destino inexistente` | Submodule no inicializado o tag incorrecto |
| Junctions | **No** usar `mklink /J`; no son portables en Git cross-platform |

## Fuera de alcance

- CLI empaquetada (`sdaf materialize`) — candidata a sdaf-core 0.3.x.
- Junctions de Windows.
