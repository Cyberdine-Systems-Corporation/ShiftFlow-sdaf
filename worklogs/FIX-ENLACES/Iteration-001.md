---
pbi: FIX-ENLACES
iteracion: Iteration-001
fecha: 2026-09-25
inicio: 2026-09-25T19:40:00+02:00
fin: 2026-09-25T20:05:00+02:00
agente: humano (pide) + Claude Code (ejecuta)
modelo: claude-sonnet-5
version_prompt: "0.2"
prompt_base: sdaf-worklog-handoff@0.4.0
prompts_adicionales: ninguno
skills: sdaf-worklog-handoff@0.4.0
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: instrucciones humanas «Puedes revisarlo?» y «Hazlo todo» sobre enlaces no navegables
contexto: Enlaces a documentos del repo no navegables. Causa 1, symlinks versionados con destino corrupto (CRLF y BOM). Causa 2, tres enlaces de handbook con la numeración antigua del extract.
especificaciones_utilizadas: sdaf-core/handbook/13-enmienda-excepciones-ciclo-de-vida.md (§6), docs/materializacion-submodules.md
archivos_leidos: scripts/materialize-submodules.ps1, handbook/11-ddd-and-bounded-contexts.md, handbook/16-testing-framework.md, skills/README.md
archivos_modificados: scripts/materialize-submodules.ps1, handbook/11-ddd-and-bounded-contexts.md, handbook/16-testing-framework.md, 53 symlinks (blobs 120000 en skills/, agents/, prompts/agents/, .cursor/), worklogs/FIX-ENLACES/Iteration-001.md, worklogs/README.md
origen_cambios: N/A
resultado: Blobs de los 53 symlinks canónicos (destino con '/', sin CRLF ni BOM); script corregido; 3 enlaces de handbook corregidos (H11 y H16 a 0.1.2).
tiempo: PT0H25M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "La shell del agente no puede crear symlinks en disco (falta privilegio); el working tree debe rematerializarse con el script desde una shell con Modo desarrollador o elevada."
pruebas_ejecutadas: "Escaneo de enlaces markdown relativos (PowerShell); comprobación de tamaño de blob por symlink (git cat-file -s); validate-worklog.py no ejecutado (sin Python en el entorno)"
estado: hecho
siguiente_agente: humano (rematerializar symlinks en disco y revisar PR)
commit: null
pr: null
rama: fix/enlaces-symlinks
sha: null
resumen_acumulado: "sdaf-worklog-handoff@0.4.0 — corrección de symlinks corruptos y enlaces de handbook; rama fix/enlaces-symlinks; PR y SHA ausentes"
---

# FIX-ENLACES / Iteration-001

## Línea de decisión

- `Set-GitSymlinkIndex` pasaba el destino por el pipe de PowerShell 5.1 a `git hash-object --stdin`, que añade CRLF (y BOM en algunos casos). Ahora escribe los bytes exactos en un fichero temporal.
- Las comprobaciones `.Trim()` ocultaban el defecto y el script daba los enlaces por correctos. Ahora un destino con BOM o salto de línea, o un blob de tamaño distinto al canónico, cuenta como incorrecto.
- Los handbooks 11 y 16 (Approved) reciben bump patch 0.1.2 con fila de historial: el cambio es editorial y no altera contenido normativo (H13 §6).

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
