# PBI-016 — Catálogo IHardRule + ISoftRule (plataforma dominio)

| Campo | Valor |
|--------|--------|
| Sprint | post-mvp-rules-v2 / plataforma |
| Prioridad | 16 |
| Specs | SPEC-DOM-008 **Approved**; SPEC-APP-006 **Approved**; SPEC-ACC-006 **Approved**; ADR-009 **Aceptado** |
| DoD | HR-01/02/03 como `IHardRule`; stubs/`ISoftRule` en catálogo; `RuleEvaluationResult`; AssignShift hard sin cambio observable; soft disabled por defecto; unit tests verdes |
| Estado | Hecho (PR 22; Gate 0 PASS; RDD aprobada) |

## Descripción

Refactor Rule Engine v1 a catálogo hard + soft. Orquestación con violaciones y warnings. Sin persistencia de config aún.

## Notas

- Soft no bloquea. Gate 0 obligatorio.
