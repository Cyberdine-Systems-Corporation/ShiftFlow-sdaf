---
pbi: UPGRADE-SDAF-0.4
iteracion: Iteration-001
fecha: 2026-09-25
inicio: 2026-09-25T11:33:35+02:00
fin: 2026-09-25T11:39:06+02:00
agente: humano + Claude Code (upgrade de gobernanza)
modelo: claude-opus-5-5
version_prompt: "0.2"
prompt_base: sdaf-upgrade@0.2.0
prompts_adicionales: ninguno
skills: sdaf-upgrade@0.2.0
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: plan aprobado por humano (Fase A del upgrade 0.2.1 → 0.4.0)
contexto: Pin sdaf-core v0.2.1 y sdaf-stack-dotnet v0.1.1; latest publicados core v0.4.0 y pack v0.3.0 (el pack 0.3.0 exige core >=0.4.0 <0.5.0).
especificaciones_utilizadas: sdaf-core/docs/adopcion-y-upgrade.md, sdaf-core/handbook/CHANGELOG.md, sdaf-core/handbook/13-enmienda-excepciones-ciclo-de-vida.md, sdaf-stack-dotnet/CHANGELOG.md
archivos_leidos: sdaf.config.yaml, AGENTS.md, sdaf-core/AGENTS.md.template, sdaf-core/sdaf.config.schema.yaml, sdaf-core/templates/security.md, sdaf-core/templates/worklog.md, sdaf-core/worklog.schema.json, sdaf-core/.github/actions/validate-sdaf/action.yml, sdaf-stack-dotnet/.github/workflows/validate.yml, scripts/materialize-submodules.ps1, CONTRIBUTING.md
archivos_modificados: sdaf-core (pin), sdaf-stack-dotnet (pin), sdaf.config.yaml, AGENTS.md, CODEOWNERS, SECURITY.md, CONTRIBUTING.md, README.md, BOOTSTRAP.md, docs/materializacion-submodules.md, scripts/materialize-submodules.ps1, scripts/materialize-submodules.sh, .github/workflows/validate-sdaf.yml, skills/{testing-review-pr,security-review,devops-ci-gate}, .cursor/skills/{testing-review-pr,security-review,devops-ci-gate}, .cursor/rules/git-remoto-encargo.mdc, worklogs/README.md, worklogs/UPGRADE-SDAF-0.4/Iteration-001.md
origen_cambios: N/A
resultado: Pins en core v0.4.0 y pack v0.3.0; sdaf.version "0.4.0"; AGENTS.md regenerado desde la plantilla 0.3.3 conservando activos, stubs, fusiones e inventario del pack; skills Parte III y regla git-remoto-encargo enlazadas; CODEOWNERS, SECURITY.md y CI validate-sdaf añadidos.
tiempo: PT0H5M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "Sin privilegio de symlink en el shell de la sesión: los 7 enlaces nuevos quedan en el índice (mode 120000) pero no en disco (git status AD); repetir materialize-submodules.ps1 -Force con Modo desarrollador. Sin backfill de worklogs en tabla ni de fechas publicadas (H13 §9)."
pruebas_ejecutadas: validate-config.py sdaf.config.yaml --strict-i4 --consumer-root .; validate-worklog.py sobre este worklog; materialize-submodules.ps1 -WhatIf
estado: hecho
siguiente_agente: humano (revisión del PR de upgrade por CODEOWNERS; después Fase B sobre el PR 11)
commit: null
pr: null
rama: chore/sdaf-core-0.4.0
sha: null
resumen_acumulado: "sdaf-upgrade@0.2.0 — core v0.2.1→v0.4.0 y pack v0.1.1→v0.3.0; rama chore/sdaf-core-0.4.0; commit, PR y SHA ausentes"
---

# UPGRADE-SDAF-0.4 / Iteration-001

## Línea de decisión

- Subir core y pack juntos: `sdaf-stack-dotnet` 0.3.0 declara `sdaf_core: ">=0.4.0 <0.5.0"` (CHANGELOG del pack, 0.3.0).
- `sdaf.version: "0.4.0"`: nombra la línea de constitución del pin `v0.4.0` (adopcion-y-upgrade, Versionado).
- Pasos 0.3.0: skills `testing-review-pr`, `security-review`, `devops-ci-gate` y `SECURITY.md` desde `templates/security.md` (adopcion-y-upgrade §0.3.0).
- Pasos 0.3.1–0.3.3: regla `git-remoto-encargo.mdc`; restricción de commit/remoto por turno en `AGENTS.md` (H06 §7).
- Pasos 0.4.0: `CODEOWNERS` = identidad nominada para aceptación y QG-Review (H13 §2 y §7, H10); worklogs nuevos con frontmatter; fechas nuevas con hora y zona (H13 §9); action `validate-sdaf` en CI (paso 5 opcional, adoptado).
- CI: I4 estricto porque el consumidor declara `stack.pack` y tiene contratos locales de los ids de extensión; la validación de worklogs solo cubre los que tienen frontmatter (sin backfill).
- No se migran specs, knowledge ni handbook de producto (sdaf-upgrade, paso 6). Historiales de ADR-001…007 y `docs/PARITY-EXTRACT.md` conservan las citas de versión que tenían.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
