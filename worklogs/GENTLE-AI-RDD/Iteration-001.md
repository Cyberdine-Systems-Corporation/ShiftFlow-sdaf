---
pbi: GENTLE-AI-RDD
iteracion: Iteration-001
fecha: 2026-09-27
inicio: 2026-09-27T08:30:00+02:00
fin: 2026-09-27T08:43:00+02:00
agente: humano + Claude Code (gobierno de tooling)
modelo: claude-opus-5-5
version_prompt: "N/A"
prompt_base: "N/A: encargo directo del humano, sin prompt de agente SDAF"
prompts_adicionales: ninguno
skills: ninguna
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: "plan aprobado por humano (híbrido 1: RDD pre-commit sin excepción H06 §7)"
contexto: "AGENTS.md 0.4.2 adoptó gentle-ai sin excepción H06 §7 y la guía del core pide entonces desactivar RDD. El humano quiere usar RDD sin relajar H06 §7: review del candidato preparado antes del commit y commit solo por encargo vigente."
especificaciones_utilizadas: sdaf-core/architecture/decisions/ADR-004-tooling-externo-de-agentes.md, sdaf-core/docs/integracion-gentle-ai.md, sdaf-core/handbook/06-ai-agent-framework.md (§7)
archivos_leidos: AGENTS.md, sdaf.config.yaml, sdaf-core/templates/worklog.md, worklogs/FIX-BOOTSTRAP/Iteration-001.md, worklogs/README.md, docs/materializacion-submodules.md
archivos_modificados: AGENTS.md, docs/piloto-rdd-hibrido.md, worklogs/README.md, worklogs/GENTLE-AI-RDD/Iteration-001.md
origen_cambios: N/A
resultado: "AGENTS.md 0.4.3 permite RDD solo antes del commit (--projection staged), sin excepción H06 §7; guion del piloto en docs/piloto-rdd-hibrido.md con criterios C1–C5, plan B y marcha atrás. RDD sigue desactivado."
tiempo: PT0H13M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "Hallazgos del --help de gentle-ai main@520ed86e8: review mode aplica «Any off wins» (global off, clone-local unset); review start --projection staged «records post-commit delivery provenance»; review assess es solo lectura; los recibos viven en el Git common directory (.git/); el stop-hook solo imprime un recordatorio y nunca ejecuta review start; store-reset es irreversible y aplica con --confirm. Sin confirmar hasta el piloto: C1 already_reviewed tras el commit, C2 stop-hook sin bloqueo, C3 árbol limpio, C4 consentimiento relay en castellano, C5 coste. La guía del core (integracion-gentle-ai.md, matriz de encaje) dice que RDD solo tiene sentido con excepción de commit; con --projection staged es inexacto; su corrección va aparte en sdaf-core."
pruebas_ejecutadas: lectura de --help de gentle-ai review, review mode, review assess, review start, review status, review stop-hook y review store-reset; gentle-ai review mode status; validate-worklog.py sobre este worklog
estado: hecho
siguiente_agente: humano (piloto de RDD y revisión por CODEOWNERS)
commit: null
pr: 17
rama: chore/gentle-ai-rdd-hibrido
sha: 625744f1ffa5f8d3c09b1a878d4b9e283bbf37ab
resumen_acumulado: "Encargo directo — RDD híbrido (review pre-commit con --projection staged) en AGENTS.md 0.4.3 y guion de piloto; rama chore/gentle-ai-rdd-hibrido; PR 17; SHA 625744f"
---

# GENTLE-AI-RDD / Iteration-001

## Línea de decisión

- Híbrido 1 elegido por el humano frente a añadir una excepción H06 §7: la review se hace sobre el candidato preparado (`--projection staged`) antes del commit, y el commit sigue exigiendo el encargo vigente.
- ADR-004 de sdaf-core, decisión 5: H06 §7 prevalece sobre el protocolo de commit del tooling; no se enumera excepción.
- ADR-004 de sdaf-core, decisión 6: el dictamen de RDD alimenta `testing-review-pr` y no es QG-Review; el merge exige revisión de `CODEOWNERS`.
- ADR-004 de sdaf-core, decisión 3: la evidencia es este worklog; Engram y los recibos de RDD son caché.
- `AGENTS.md` sube a 0.4.3 con la cláusula RDD en «Tooling externo» y enlaza el guion `docs/piloto-rdd-hibrido.md`.
- RDD sigue desactivado hasta el piloto. Activarlo (`gentle-ai review mode enable`) es decisión humana.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
