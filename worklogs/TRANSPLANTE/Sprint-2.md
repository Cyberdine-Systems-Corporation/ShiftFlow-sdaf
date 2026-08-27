# Worklog — TRANSPLANTE / Sprint-2

| Campo | Valor |
|--------|--------|
| Fecha | 2026-08-27 |
| Agente | domain-application / testing-review |
| Skills | `csharp-adr006-slice@0.1.0` |
| Fuente | ShiftFlow-sdaf-extract (mismo árbol que Sprint-0) |
| Estado | hecho |
| PBIs | PBI-005, PBI-006, PBI-007, PBI-009, PBI-014 |

## Alcance presente en el árbol

- Calendario / AssignShift / CancelShift
- Rule Engine HR-01…03
- Leave + HR-02
- Acceptance API (journey + ACC-*)
- Migraciones EF (`InitialCreate`) + `MigrateAsync` (ADR-007)

## Verificación

Cubierta por UnitTests (reglas/leave) + IntegrationTests (calendar, leave, journey) — ver Sprint-0.
