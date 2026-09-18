# PBI-016 — Catálogo IHardRule (plataforma dominio)

| Campo | Valor |
|--------|--------|
| Sprint | post-mvp-rules-v2 / plataforma |
| Prioridad | 16 |
| Specs | [SPEC-DOM-008](../specs/domain/SPEC-DOM-008-rule-engine-v2-catalog.md) **Draft**; [SPEC-APP-006](../specs/application/SPEC-APP-006-rule-config-use-cases.md) **Draft**; [SPEC-ACC-006](../specs/acceptance/SPEC-ACC-006-rule-catalog-config.md) **Draft**; ADR-009 **Propuesto** |
| DoD | HR-01/02/03 como `IHardRule`; AssignShift sin cambio observable; unit tests verdes |
| Estado | Pendiente (Gate 0: Approved + ADR Aceptado) |

## Descripción

Refactor del Rule Engine v1 a catálogo interno: `IHardRule`, `RuleEvaluationContext`, orquestación. Sin persistencia de config aún (defaults = siempre las tres reglas como hoy).

## Notas

- No UI. DI del motor desde Application.
- Gate 0 obligatorio; no implementar en Draft.
