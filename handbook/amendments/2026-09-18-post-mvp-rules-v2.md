# Enmienda — corte `post-mvp-rules-v2`

| Campo | Valor |
|--------|--------|
| **Estado** | Draft (pendiente aprobación humana) |
| **Fecha** | 2026-09-18 |
| **Norma superior** | [03-mvp-definition.md](../03-mvp-definition.md), [04-product-roadmap.md](../04-product-roadmap.md), [02-product-vision.md](../02-product-vision.md) |
| **ADR** | [ADR-009](../../architecture/decisions/ADR-009-rule-engine-v2-catalogo-hard.md) (Propuesto) |
| **Specs** | SPEC-PRD-004, SPEC-DOM-008, SPEC-APP-006, SPEC-ACC-006 (Draft) |

---

## 1. Motivación

El MVP `mvp-0.1` está cerrado (backlog PBI-001…015 Hecho; paridad extract firmada). El roadmap post-MVP prioriza ampliar el Rule Engine. Esta enmienda fija el **primer corte post-MVP** con **hard y soft configurables** en el mismo catálogo, sin abrir motores Optimization/Compliance separados.

---

## 2. Nombre del corte

`post-mvp-rules-v2`

---

## 3. Incluido (In)

| Tema | Detalle |
|------|---------|
| Catálogo hard | `IHardRule` / códigos `HR-xx`; **bloquean** persistencia de `AssignShift` |
| Catálogo soft | `ISoftRule` / códigos `SR-xx`; **no bloquean**; producen avisos (`RuleWarning`) |
| Resultado de evaluación | `RuleEvaluationResult { HardViolations[], SoftWarnings[] }` |
| AssignShift | Si hay hard → no persistir (400). Si solo soft → **persistir** y devolver avisos en la respuesta de éxito |
| Config por Organization | Enable/disable + params para hard y soft; **HR-01 siempre activa** |
| Migración v1 | HR-02/HR-03 al catálogo; default = MVP |
| Hard nuevas | HR-04…HR-08 según knowledge curated + SPEC-DOM-008 |
| Soft piloto | SR-01, SR-02 (preferencias); ver knowledge curated |
| UI | **Mínima**: admin enable/params (hard+soft) + mostrar avisos soft en Calendar al asignar. Sin rediseño global |
| Explain | Stub ampliado a `HR-xx` y `SR-xx` (sin mutar cuadrante) |
| Tests | Unit Domain + ACC API (hard, soft, config) |

---

## 4. Excluido (Out) en este corte

- Fairness scoring avanzado / motor de puntuación global (más allá de soft piloto SR-*).
- Optimization Engine; IA que genere o persista cuadrantes.
- Compliance Engine como BC o servicio aparte.
- Motor de reglas externo (Drools, DSL).
- Soft que bloquee persistencia (sería hard).
- MAUI, SignalR, multitenancy, ERP/AD.
- UI avanzada de catálogo (matriz masiva, preview de impacto, historial de auditoría de config).

---

## 5. Relación con el MVP

- No invalida el DoD de `mvp-0.1`.
- Journey demo MVP verde con **config por defecto** (hard = v1; soft default off o sin avisos en journey canónico).
- ADR-003 sigue vigente para el MVP; ADR-009 lo **extiende** para este corte.

---

## 6. Criterios de aceptación de la enmienda

1. Existe ADR-009 Propuesto/Aceptado alineado a hard+soft.
2. Specs Draft del catálogo enlazan PBIs 016–025.
3. Optimization y fairness avanzado permanecen Out; soft piloto In.
4. Tras aprobación humana: actualizar estado de esta enmienda y, si aplica, historial de H02/H03/H04.

---

## 7. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.2.0 | 2026-09-18 | In: soft + hard en el mismo corte; Out: fairness avanzado / Optimization |
| 0.1.0 | 2026-09-18 | Draft inicial Fase 0 (solo hard) |
