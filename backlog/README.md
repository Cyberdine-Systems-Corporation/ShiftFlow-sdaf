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

## Post-MVP — `post-mvp-rules-v2` (Draft gobernanza)

Enmienda: [`handbook/amendments/2026-09-18-post-mvp-rules-v2.md`](../handbook/amendments/2026-09-18-post-mvp-rules-v2.md).  
Gate 0: specs Approved + ADR-009 Aceptado antes de código. **Hard + soft** en el mismo corte.

| Orden | ID | Título | Oleada | Specs / ADR | Estado |
|------:|----|--------|--------|-------------|--------|
| 16 | [PBI-016](PBI-016-rule-catalog-ihardrule.md) | Catálogo IHardRule + ISoftRule | Plataforma | DOM-008, APP-006, ACC-006, ADR-009 | Pendiente |
| 17 | [PBI-017](PBI-017-organization-rule-config.md) | OrganizationRuleConfig + UI mínima | Plataforma | DOM-008, APP-006, ACC-006 | Pendiente |
| 18 | [PBI-018](PBI-018-hr04-night-parity.md) | HR-04 Noches pares/impares | Hard 1 | DOM-008 §4.2 | Pendiente |
| 19 | [PBI-019](PBI-019-hr05-monthly-hour-bag.md) | HR-05 Bolsa mensual | Hard 1 | DOM-008 §4.2 | Pendiente |
| 20 | [PBI-020](PBI-020-hr06-max-consecutive-days.md) | HR-06 Racha máxima | Hard 1 | DOM-008 §4.2 | Pendiente |
| 21 | [PBI-021](PBI-021-hr07-night-quotas.md) | HR-07 Cuotas nocturnas | Hard 2 | DOM-008 §4.2 | Pendiente |
| 22 | [PBI-022](PBI-022-hr08-intermonth.md) | HR-08 Intermensual | Hard 2 | DOM-008 §4.2 (bloqueado) | Pendiente |
| 23 | [PBI-023](PBI-023-soft-warnings-assign-ui.md) | Avisos soft AssignShift/Calendar | Plataforma | APP-006, ACC-006 | Pendiente |
| 24 | [PBI-024](PBI-024-sr01-weekend-preference.md) | SR-01 Preferencia fin de semana | Soft | DOM-008 §5 | Pendiente |
| 25 | [PBI-025](PBI-025-sr02-shift-type-preference.md) | SR-02 Preferencia tipología | Soft | DOM-008 §5 | Pendiente |

## Leyenda

| Estado PBI | Significado |
|------------|-------------|
| Pendiente | Código aún no trasplantado a este repo |
| En curso | Worklog de trasplante abierto |
| Hecho | DoD + verificación build/test en este repo |
