# PBI-003 — Organization, Department, Employee

| Campo | Valor |
|--------|--------|
| Sprint | 1 |
| Prioridad | 3 |
| Specs | SPEC-PRD-001 (C-ORG, C-DEP, C-EMP), SPEC-DOM-001/002, SPEC-APP-001, SPEC-ACC-001 (**Draft**) |
| DoD | Persistencia + API + tests de aggregates críticos |
| Estado | Hecho |

## Descripción

Maestros de estructura organizativa y empleados asignables.

## Notas de implementación

- Aggregates `Organization`, `Department` (AR con `OrganizationId`), `Employee`.
- CQRS vía MediatR; endpoints bajo `/api/organizations|departments|employees`.
- UI Blazor CRUD diferida a PBI-008; ShiftType a PBI-004 (ACC-S1-03 completo con STT ahí).
---

## Importación (2026-08-25)

Importado desde ShiftFlow-sdaf-extract. Estado reseteado a **Pendiente** — reimplementar en este repo tras Gate 0 (specs Approved + ADRs Aceptado + worklog).

---

## Trasplante 2026-08-27

Código/tests verificados en este repo (worklogs/TRANSPLANTE/). DoD cubierto por build+tests; sin reescritura.
