---
pbi: UPGRADE-SDAF-0.4
iteracion: Iteration-002
fecha: 2026-09-26
inicio: 2026-09-26T22:55:00+02:00
fin: 2026-09-26T23:20:00+02:00
agente: humano + Claude Code (upgrade de gobernanza)
modelo: claude-opus-5-5
version_prompt: "0.2"
prompt_base: sdaf-upgrade@0.2.0
prompts_adicionales: ninguno
skills: sdaf-upgrade@0.2.0, sdaf-bootstrap@0.4.2
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: plan aprobado por humano (revisión de cambios en sdaf-core y sdaf-stack-dotnet; adopción de gentle-ai)
contexto: Pin sdaf-core v0.4.0 y sdaf-stack-dotnet v0.3.0; publicados core v0.4.1 y v0.4.2 (ADR-004 de sdaf-core, bloque tooling) y pack main sin tag (solo docs y CI, [Unreleased]).
especificaciones_utilizadas: sdaf-core/handbook/CHANGELOG.md, sdaf-core/docs/adopcion-y-upgrade.md, sdaf-core/architecture/decisions/ADR-004-tooling-externo-de-agentes.md, sdaf-core/docs/integracion-gentle-ai.md, sdaf-core/sdaf.config.schema.yaml, sdaf-stack-dotnet (compare v0.3.0...main)
archivos_leidos: sdaf.config.yaml, AGENTS.md, sdaf-core/AGENTS.md.template, sdaf-core/sdaf.config.schema.json, sdaf-core/scripts/validate-config.py, sdaf-core/skills/sdaf-bootstrap/SKILL.md, .github/workflows/validate-sdaf.yml, scripts/materialize-submodules.ps1, .gitignore
archivos_modificados: sdaf-core (pin), sdaf.config.yaml, AGENTS.md, .gitignore, .github/workflows/validate-sdaf.yml, README.md, BOOTSTRAP.md, docs/materializacion-submodules.md, worklogs/README.md, worklogs/UPGRADE-SDAF-0.4/Iteration-002.md
origen_cambios: N/A
resultado: Core en v0.4.2 (pack sigue en v0.3.0); tooling.gentle_ai declarado con SHA 520ed86e8; AGENTS.md 0.4.2 con la sección «Tooling externo» sin excepción H06 §7; .engram/, odd/ y .atl/ ignorados; action validate-sdaf fijada a v0.4.2.
tiempo: PT0H25M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "Instalación de gentle-ai pendiente del humano (go install fijado al SHA, gentle-ai install --preset custom sin sdd, --scope global, para claude-code y cursor; review mode disable). Tras instalar, confirmar con skill-registry list que sigue los symlinks de skills/ y qué rutas escribe en el repo. BOOTSTRAP.md:55 cita sdaf-stack-dotnet@0.1.0 (obsoleto, fuera de alcance)."
pruebas_ejecutadas: validate-config.py sdaf.config.yaml --strict-i4 --consumer-root .; validate-worklog.py sobre este worklog
estado: hecho
siguiente_agente: humano (instalación de gentle-ai y revisión por CODEOWNERS)
commit: null
pr: null
rama: chore/sdaf-core-0.4.2-gentle-ai
sha: null
resumen_acumulado: "sdaf-upgrade@0.2.0 — core v0.4.0→v0.4.2 y adopción de gentle-ai (tooling.gentle_ai); rama chore/sdaf-core-0.4.2-gentle-ai; commit, PR y SHA ausentes"
---

# UPGRADE-SDAF-0.4 / Iteration-002

## Línea de decisión

- Pack en v0.3.0: `main` no tiene tag nuevo y solo cambia docs y CI; `sdaf_core: ">=0.4.0 <0.5.0"` ya cubre v0.4.2 (CHANGELOG del pack, [Unreleased]).
- Subir el core a v0.4.2 es requisito de la adopción: el schema de v0.4.0 cierra la raíz con `additionalProperties: false` y rechazaría `tooling` (sdaf.config.schema.json).
- `sdaf.version` sigue en `"0.4.0"`: nombra la línea, no el parche (adopcion-y-upgrade §0.4.1 y §0.4.2).
- `tooling.gentle_ai` = SHA `520ed86e8c598b01f439e28c34d391cd6f1744e3`, no una rama ni v3.7.0: es el merge que retira SDD y el que revisa la guía (integracion-gentle-ai, Adopción opcional). Decisión humana.
- Sin excepción H06 §7: ODD no crea commits sin encargo y RDD queda desactivado (ADR-004 de sdaf-core, decisión 5). Decisión humana.
- Engram no se versiona: es caché, no evidencia (ADR-004 de sdaf-core, decisión 3; H08). Decisión humana.
- Sección de `AGENTS.md` desde `AGENTS.md.template` 0.4.2 (sdaf-upgrade paso 4; sdaf-bootstrap@0.4.2 paso 3).
- Sin rematerializar: v0.4.1–v0.4.2 no añaden skills, agentes, prompts ni reglas.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
