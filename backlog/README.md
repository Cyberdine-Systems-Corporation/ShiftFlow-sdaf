# Backlog MVP (`mvp-0.1`)

Priorizado según [`handbook/04-product-roadmap.md`](../handbook/04-product-roadmap.md) (Approved).

Trasplante desde `ShiftFlow-sdaf-extract` (2026-08-27): código y tests en este repo; ver `worklogs/TRANSPLANTE/` y [`docs/PARITY-EXTRACT.md`](../docs/PARITY-EXTRACT.md).

## Orden de prioridad

| Orden | ID | Título | Sprint | Specs / ADR | Estado |
|------:|----|--------|--------|-------------|--------|
| 1 | [PBI-001](PBI-001-skeleton-solucion.md) | Skeleton solución .NET + runtime local | 0 | SPEC-PRD-001, ADR-001, ADR-004 | Hecho |
| 2 | [PBI-002](PBI-002-auth-roles.md) | Auth y roles básicos | 1 | SPEC-DOM-004, SPEC-APP-002, SPEC-ACC-001, ADR-005 | Hecho |
| 3 | [PBI-003](PBI-003-organization-department-employee.md) | Organization, Department, Employee | 1 | SPEC-DOM-001/002, SPEC-APP-001, SPEC-ACC-001 | Hecho |
| 4 | [PBI-004](PBI-004-shift-types.md) | Shift types | 1 | SPEC-DOM-003, SPEC-APP-001, SPEC-ACC-001 | Hecho |
| 5 | [PBI-005](PBI-005-calendar-assign-shift.md) | Calendario y asignación manual | 2 | SPEC-DOM-005/006, SPEC-APP-003, SPEC-ACC-002, ADR-003 | Hecho |
| 6 | [PBI-006](PBI-006-rule-engine-v1.md) | Rule Engine v1 (3 hard rules) | 2 | SPEC-DOM-006/007, SPEC-ACC-004, ADR-003 | Hecho |
| 7 | [PBI-007](PBI-007-leaves.md) | Leaves / ausencias | 2 | SPEC-DOM-007, SPEC-APP-004, SPEC-ACC-003 | Hecho |
| 8 | [PBI-008](PBI-008-blazor-shell-crud.md) | Blazor shell + CRUD maestros | 1–2 | SPEC-PRD-002, SPEC-APP-001 | Hecho |
| 9 | [PBI-009](PBI-009-acceptance-tests-demo.md) | Acceptance tests del journey | 2–3 | SPEC-PRD-002, SPEC-ACC-* | Hecho |
| 10 | [PBI-010](PBI-010-runbook-demo-freeze.md) | Runbook, seed opcional, freeze demo | 3 | SPEC-PRD-001/002, ADR-007 | Hecho |
| 11 | [PBI-011](PBI-011-ai-explain-stub.md) | Stub IA explicación de reglas | 3 | SPEC-APP-005, SPEC-ACC-005, ADR-003 | Hecho |
| 12 | [PBI-012](PBI-012-presentacion-slides-video.md) | Slides + vídeo de presentación | 3 | SPEC-PRD-001 (C-PRE) | Hecho |
| 13 | [PBI-013](PBI-013-ux-blazor-redesign.md) | Rediseño UX demo (Blazor) | 2–3 | SPEC-PRD-003 | Hecho |
| 14 | [PBI-014](PBI-014-ef-migrations.md) | Migraciones EF Core | 2–3 | SPEC-PRD-001, ADR-007 | Hecho |
| 15 | [PBI-015](PBI-015-ux-ia-freeze.md) | Jerarquía UX freeze (Blazor) | 3 | SPEC-PRD-003 | Hecho |

## Leyenda

| Estado PBI | Significado |
|------------|-------------|
| Pendiente | Código aún no trasplantado a este repo |
| En curso | Worklog de trasplante abierto |
| Hecho | DoD + verificación build/test en este repo |
