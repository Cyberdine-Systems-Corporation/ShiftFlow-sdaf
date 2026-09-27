---
pbi: GENTLE-AI-RDD
iteracion: Iteration-002
fecha: 2026-09-27
inicio: 2026-09-27T09:40:00+02:00
fin: 2026-09-27T10:26:00+02:00
agente: "humano + Claude Code (piloto de tooling)"
modelo: claude-opus-5-5
version_prompt: "N/A"
prompt_base: "N/A: encargo directo del humano, sin prompt de agente SDAF"
prompts_adicionales: ninguno
skills: ninguna
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: "encargos del humano: «Piloto», «haz el commit», «Opción 1» (RDD antes del PR)"
contexto: "Piloto del RDD híbrido de Iteration-001 (review del candidato preparado antes del commit, sin excepción H06 §7) en la rama chore/piloto-rdd, con RDD activado en el global por decisión humana. El piloto mostró que la review previa al commit no se enlaza al commit; el humano elige RDD antes del PR."
especificaciones_utilizadas: sdaf-core/architecture/decisions/ADR-004-tooling-externo-de-agentes.md, sdaf-core/docs/integracion-gentle-ai.md, sdaf-core/handbook/06-ai-agent-framework.md (§7)
archivos_leidos: AGENTS.md, docs/piloto-rdd-hibrido.md, worklogs/GENTLE-AI-RDD/Iteration-001.md, worklogs/README.md, sdaf-core/templates/worklog.md
archivos_modificados: worklogs/GENTLE-AI-RDD/Iteration-001.md (commit 819c5f5), AGENTS.md, docs/piloto-rdd-hibrido.md, worklogs/README.md, worklogs/GENTLE-AI-RDD/Iteration-002.md
origen_cambios: N/A
resultado: "Piloto hecho: C1 y C2 fallan, C3 y C5 pasan, C4 parcial (el consentimiento llega, pero en inglés). Por decisión humana (Opción 1), RDD pasa a revisar el rango commiteado antes del PR, sin excepción H06 §7: AGENTS.md 0.4.4 y docs/piloto-rdd-hibrido.md reescrito como flujo operativo y resultados. RDD sigue activo en el global."
tiempo: PT0H46M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "Piloto del 2026-09-27T09:43–09:57+02:00, candidato pr 17 + sha en Iteration-001 (6 líneas, staged), risk passive (non_executable_only). C1 ❌: tras el commit 819c5f5 (árbol 485029a, igual al revisado), assess --base-ref 437d745 --committed-only da review_due_reason passive y candidato base-diff con consumed false; el STATUS del rango da fresh_target_ready con target_identity nuevo (ba2ff37…): mismo base_tree, candidate_tree y paths_digest, pero la identidad incluye el tipo de candidato (current-changes frente a base-diff). C2 ❌: el stop-hook llega a Claude Code como error bloqueante, usa el preflight sin selector (contrato v2, proyección workspace), no reconoce la review staged y exige la suya; se dispara en cada cierre de turno con cambios sin commit, incluso con trabajo a medias o de un subagente en segundo plano (visto de nuevo a las 10:00 sobre el candidato intermedio c2a2ad3f…, que no se revisó por ser intermedio). C3 ✅: recibos en .git/gentle-ai/review-transactions/v2/ y git status limpio. C4 con el candidato passive del piloto: sin ejercitar, porque un cambio passive no pasa lentes ni pide consentimiento (el resultado final de C4 es parcial, ver más abajo). C5 ✅: segundos, solo por ser passive. Tres reviews para un único cambio: review-39a32bb0b9ec3599 (staged, v1, guion), review-7f2aa92d5834cf19 (current-changes workspace, 1.er disparo del stop-hook), review-0f025da387926c33 (base-diff 437d745..819c5f5, 2.º disparo). El guion anterior fallaba en el paso 5: start --agent exige --contract, y un start negociado exige --target y --projection, que solo da el preflight. Coste estructural: bajo H06 §7 el agente suele cerrar el turno con cambios sin commit; el stop-hook revisa entonces current-changes y, tras el commit, base-diff: 2 reviews por cambio. Solo 1 si implementación y commit se ordenan en el mismo turno o si el turno termina sin cambios nuevos sin commitear. Gratis para passive; con lentes, doble. El commit 819c5f5 es el del piloto y solo contiene Iteration-001; los cambios de esta iteración (AGENTS.md, guion, README y este worklog) estuvieron sin commit hasta d89be93, hecho por orden humana; commit y sha apuntan a d89be93. Review con lentes del cambio de la Opción 1: el preflight pidió declarar Iteration-002.md, que no tenía seguimiento (intended_untracked_selection_required); start devolvió consent_required (riesgo medium por executable_change en AGENTS.md), trasladado al humano en inglés y respondido granted; review-e3475e7f33d2f7da con la lente review-reliability (59 s): aprobada con 3 hallazgos no bloqueantes (R3-worklog-sha-mismatch, R3-stophook-intermediate-deadlock, R3-c4-lenses-unproved), corregidos después en este worklog y en el guion; confirmada. Segunda review con lentes del cambio corregido, review-049d46d453de68c4 (46 s, consentimiento granted): aprobada con 3 hallazgos no bloqueantes (R3-c4-status-contradiction, R3-pinned-range-flags-unverified, R3-lens-review-commands-unrecorded), corregidos junto al commit por orden humana para que una sola review del rango los cubra. C4 queda parcial. Tras el commit d89be93, la review del rango 437d745..d89be93 (base-diff, --committed-only) fue review-6e6ff05d9acb94b2 (consentimiento granted, lente review-reliability, 73 s): aprobada y confirmada (target_already_acknowledged) con 4 hallazgos no bloqueantes citados en el PR 18 y corregidos en Iteration-003. Es la review que cubre las correcciones de la segunda review."
pruebas_ejecutadas: "gentle-ai review mode status; gentle-ai review mode enable (humano); git add + git status; gentle-ai review assess --cwd . --json; gentle-ai review start --projection staged … (falla: exige --contract); gentle-ai review status --agent claude-code --contract gentle-ai.review-integration/v1 --projection staged --next-transition; review start verbatim del next_transition (review-39a32bb0b9ec3599); gentle-ai review status --gate pre-commit; acknowledge-approved verbatim; preflight del stop-hook (gentle-ai review status --cwd <repo> --contract gentle-ai.review-integration/v2 --agent claude-code --next-transition) con start y acknowledge-approved verbatim (review-7f2aa92d5834cf19); git commit 819c5f5 (orden humana); gentle-ai review assess --cwd . --base-ref 437d745 --committed-only --json; gentle-ai review status --base-ref 437d745 --committed-only --next-transition; preflight del stop-hook tras el commit con start y acknowledge-approved verbatim (review-0f025da387926c33); git status; gentle-ai review status --base-ref 437d745 --committed-only --next-transition (target_already_acknowledged, 10:00); reviews con lentes del cambio de la Opción 1: gentle-ai review status --cwd <repo> --contract gentle-ai.review-integration/v2 --agent claude-code --next-transition (intended_untracked_selection_required), el mismo con --projection workspace --untracked-scope select --intended-untracked worklogs/GENTLE-AI-RDD/Iteration-002.md --expected-untracked-inventory <sha256> (fresh_target_ready), review start verbatim (consent_required, trasladado al humano), review start … --consent granted (respuesta humana), review status verbatim (reviewer_results_required), gentle-ai review capture-result … --lens=review-reliability --agent=claude-code y acknowledge-approved verbatim, dos veces (review-e3475e7f33d2f7da y review-049d46d453de68c4); gentle-ai review status --contract gentle-ai.review-integration/v2 --agent claude-code --base-ref 437d745 --committed-only --next-transition con cambios sin commitear (operation_failed, 10:24); validate-worklog.py sobre este worklog; git commit d89be93 (orden humana); review del rango: gentle-ai review status --cwd <repo> --contract gentle-ai.review-integration/v2 --agent claude-code --next-transition (base-diff 437d745..d89be93, fresh_target_ready), review start verbatim (consent_required, trasladado al humano), review start … --consent granted (respuesta humana), review status verbatim, gentle-ai review capture-result … --lens=review-reliability --agent=claude-code y acknowledge-approved verbatim (review-6e6ff05d9acb94b2, target_already_acknowledged); git push -u origin chore/piloto-rdd; gh pr create (PR 18)"
estado: hecho
siguiente_agente: "humano (hecho: PR 18 fusionado, merge 471cd15; hallazgos de su review en Iteration-003)"
commit: "d89be93"
pr: 18
rama: chore/piloto-rdd
sha: d89be932d5b903c42fdc2eb75ffd5f2f96e3d05e
resumen_acumulado: "Encargo directo — piloto de RDD: C1 y C2 fallan, C4 parcial; RDD antes del PR (Opción 1) en AGENTS.md 0.4.4 y docs/piloto-rdd-hibrido.md; rama chore/piloto-rdd; commits 819c5f5 (piloto, solo Iteration-001) y d89be93; review del rango review-6e6ff05d9acb94b2; PR 18"
---

# GENTLE-AI-RDD / Iteration-002

## Línea de decisión

- C1 ❌: la review previa al commit no se enlaza al commit, porque el `target_identity` incluye el tipo de candidato. Entre repetir la review tras el commit, revisar solo el workspace o volver a RDD desactivado, el humano elige la **Opción 1**: RDD revisa el rango commiteado antes del push o del PR.
- H06 §7 sin excepción: el commit, el push y el PR siguen exigiendo orden humana en el turno vigente. La review va entre el commit ordenado y el push o PR.
- ADR-004 de sdaf-core, decisión 5: H06 §7 prevalece sobre el protocolo de commit del tooling.
- ADR-004 de sdaf-core, decisión 6: el dictamen de RDD alimenta `testing-review-pr` y se cita en el PR; no es QG-Review y el merge exige revisión de `CODEOWNERS`.
- ADR-004 de sdaf-core, decisión 3: la evidencia es este worklog; los recibos de `.git/gentle-ai/` y Engram son caché.
- `AGENTS.md` pasa a 0.4.4; `docs/piloto-rdd-hibrido.md` pasa a ser el flujo operativo («RDD antes del PR») con los resultados del piloto. El flujo `staged` queda solo como resultado, no como recomendación.
- Stop-hook: no se revisan candidatos intermedios. Si bloquea sobre uno, se deja constancia y se cierra el turno, porque avisa una vez por candidato; se atiende sobre el candidato final, antes de dar el trabajo por terminado (hallazgo R3-stophook-intermediate-deadlock de `review-e3475e7f33d2f7da`).
- `commit` y `sha` en `null`: los cambios de esta iteración aún no tienen commit (hallazgo R3-worklog-sha-mismatch).
- RDD sigue activo en el global por decisión humana. La corrección de `sdaf-core/docs/integracion-gentle-ai.md` va aparte, en sdaf-core.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
