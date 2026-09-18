# PBI-016 — Catálogo IHardRule + ISoftRule (plataforma dominio)

| Campo | Valor |
|--------|--------|
| Sprint | post-mvp-rules-v2 / plataforma |
| Prioridad | 16 |
| Specs | SPEC-DOM-008 **Draft**; SPEC-APP-006 **Draft**; SPEC-ACC-006 **Draft**; ADR-009 **Propuesto** |
| DoD | HR-01/02/03 como `IHardRule`; stubs/`ISoftRule` en catálogo; `RuleEvaluationResult`; AssignShift hard sin cambio observable; soft disabled por defecto; unit tests verdes |
| Estado | Pendiente (Gate 0: Approved + ADR Aceptado) |

## Descripción

Refactor Rule Engine v1 a catálogo hard + soft. Orquestación con violaciones y warnings. Sin persistencia de config aún.

## Notas

- Soft no bloquea. Gate 0 obligatorio.
