---
pbi: DOCS-BADGE-GENTLE-AI
iteracion: Iteration-001
fecha: 2026-09-27
inicio: 2026-09-27T20:40:00+02:00
fin: 2026-09-27T20:51:00+02:00
agente: "humano + Claude Code (documentación)"
modelo: claude-opus-5-5
version_prompt: "N/A"
prompt_base: "N/A: encargo directo del humano, sin prompt de agente SDAF"
prompts_adicionales: ninguno
skills: ninguna
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: "encargo del humano: incluir el distintivo «Built with Gentle-AI» en la cabecera del README donde quede mejor, teniendo en cuenta si se usa; después «Commit y PR»"
contexto: "gentle-ai se usa en este repo como tooling de entorno desde sdaf-core v0.4.2 (tooling.gentle_ai en sdaf.config.yaml, RDD antes del PR en AGENTS.md); el MVP del producto se construyó antes de adoptarlo."
especificaciones_utilizadas: sdaf.config.yaml, AGENTS.md (Tooling externo), README de gentle-ai (sección «Built with Gentle-AI»)
archivos_leidos: README.md, sdaf.config.yaml
archivos_modificados: README.md, worklogs/README.md, worklogs/DOCS-BADGE-GENTLE-AI/Iteration-001.md
origen_cambios: N/A
resultado: "Distintivo «Built with Gentle-AI» en la cabecera del README, tras el párrafo de SDAF y precedido de una frase que lo acota al tooling de agentes; se conserva el shield de versión gentle-ai 520ed86."
tiempo: PT0H11M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "La imagen mide 900×389, así que no va en la fila de shields: se usa el fragmento HTML que recomienda gentle-ai (width 220) con la URL de la imagen sin cambios. La frase previa evita atribuir el MVP a gentle-ai, adoptado después. Review RDD del cambio sin commit: review-c28eb46c7a2af320 (passive, non_executable_only), aprobada y confirmada."
pruebas_ejecutadas: "curl de la imagen (200, image/png, 900×389); comprobación de destinos de enlace (AGENTS.md#tooling-externo-gentle-ai, docs/piloto-rdd-hibrido.md); recuento CR/LF; preflight RDD con start y acknowledge-approved verbatim; validate-worklog.py sobre este worklog"
estado: hecho
siguiente_agente: "humano (revisión del PR por CODEOWNERS)"
commit: null
pr: null
rama: docs/badge-gentle-ai
sha: null
resumen_acumulado: "Encargo directo — distintivo Built with Gentle-AI en la cabecera del README; rama docs/badge-gentle-ai; commit, PR y SHA ausentes"
---

# DOCS-BADGE-GENTLE-AI / Iteration-001

## Línea de decisión

- Tras el párrafo de SDAF y no en la fila de shields: la imagen es grande y el fragmento HTML de gentle-ai fija `width="220"`.
- Frase previa que acota el uso: gentle-ai es tooling de entorno subordinado a SDAF desde sdaf-core v0.4.2 (ADR-004 de sdaf-core, decisiones 2 y 3); el MVP es anterior.
- Se mantiene el shield `gentle-ai 520ed86`: indica el pin de `tooling.gentle_ai`.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
