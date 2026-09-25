# ADR-009 — Rule Engine v2: catálogo hard + soft configurable

| Campo | Valor |
|--------|--------|
| Estado | Aceptado |
| Fecha | 2026-09-25T19:28+02:00 |
| Decisores | Propone: agente Architecture. Acepta: identidad de [`CODEOWNERS`](../../CODEOWNERS) (sdaf-core H13 §2) |
| Aceptación | 2026-09-25T19:28+02:00 — @mortiz-iadev (CODEOWNERS), por instrucción humana explícita «Aprueba ADR-009» (sdaf-core H13 §2) |
| Relacionado | ADR-003, `handbook/amendments/2026-09-18-post-mvp-rules-v2.md`, SPEC-DOM-008, PBI-016…025 |

---

## Contexto

ADR-003 fijó Rule Engine v1 (≤3 hard fijas dentro del BC) para el MVP. El MVP está cerrado. El roadmap pide ampliar reglas (hard y soft) desde knowledge sin abrir cinco motores.

Hace falta un **catálogo interno configurable** que distinga bloqueo (hard) de aviso (soft).

---

## Decisión

1. **Rule Engine v2** = catálogos `IHardRule` + `ISoftRule` + orquestador `RuleEngine` **dentro** del BC WorkforceScheduling (no microservicio, no BC Compliance).
2. **Resultado:** `RuleEvaluationResult` con `HardViolations` y `SoftWarnings`. Hard no vacío ⇒ no persistir. Solo soft ⇒ persistir + exponer avisos.
3. **Configuración** por Organization (`OrganizationRuleConfig`: Code, Kind=Hard\|Soft, Enabled, Params). Persistencia EF; params JSON versionado por `Code`.
4. **HR-01** mandatory (siempre on). Resto hard/soft opt-in (defaults: hard MVP on; soft piloto off salvo seed de demo documentado).
5. **Optimization, Drools/DSL, fairness scoring avanzado:** Out de este ADR.
6. **UI:** mínima (admin enable/params hard+soft; avisos soft en Calendar). Sin redesign.
7. **Explain:** puerto stub; plantillas `HR-xx` y `SR-xx`.
8. Separar “Compliance Engine” solo con **nuevo ADR** si el catálogo supera umbral acordado en retrospectiva.

ADR-003 permanece válido para el MVP; este ADR lo **extiende** para `post-mvp-rules-v2`.

---

## Alternativas consideradas

| Alternativa | Motivo de rechazo / aplazamiento |
|-------------|----------------------------------|
| Seguir con clase monolítica | Frágil; sin config por org |
| Drools / motor externo | Infra injustificada |
| Compliance BC | Sobre-diseño (handbook 11) |
| Solo hard en este corte | Rechazada: producto pide soft+hard juntos |
| Soft que bloquea | Confunde severidad; soft no bloquea |

---

## Consecuencias

### Positivas

- Un solo modelo mental: catálogo + Kind + Evaluate.
- Demo de “asignar con avisos” sin romper journey hard.
- Extensión por PBI/regla.

### Negativas / costes

- Contrato API de AssignShift debe admitir `warnings` en éxito.
- Calendar mínimo debe mostrar avisos.
- Más superficie de ACC (hard, soft, config).
- Curación DOCX para HR-04…08 y SR-*.

### Cumplimiento

- Gate 0: specs Approved + este ADR Aceptado antes de PBI-016+.
- Tests ACC-006 (plataforma) + ACC soft + regresión journey.

---

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.3.0 | 2026-09-25T19:28+02:00 | Aceptado por @mortiz-iadev (CODEOWNERS); sin cambio de decisión respecto a 0.2.1 |
| 0.2.1 | 2026-09-25T18:42+02:00 | Realineado a sdaf-core 0.4.0 (redacción, sin cambio de decisión): fechas con hora (H13 §9), aceptante = CODEOWNERS (H13 §2); typo en Alternativas |
| 0.2.0 | 2026-09-18T23:06+02:00 | Incluye soft (`ISoftRule`, warnings); fairness avanzado Out |
| 0.1.0 | 2026-09-18T22:47+02:00 | Propuesto solo hard |
