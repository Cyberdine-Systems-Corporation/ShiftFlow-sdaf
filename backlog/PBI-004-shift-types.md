# PBI-004 — Shift types

| Campo | Valor |
|--------|--------|
| Sprint | 1 |
| Prioridad | 4 |
| Specs | SPEC-PRD-001 (C-STT), SPEC-DOM-003, SPEC-APP-001, SPEC-ACC-001 (**Draft**) |
| DoD | Catálogo de tipos de turno usable en asignación |
| Estado | Hecho |

## Descripción

Gestionar tipos de turno del catálogo.

## Notas de implementación

- Aggregate `ShiftType` por Organization; Name/Code únicos; End > Start sin overnight (INV-STT-04).
- API: `POST/GET /api/organizations/{id}/shift-types`, `PUT /api/shift-types/{id}`, `PUT .../active`.
- UI Blazor diferida a PBI-008.
---

## Importación (2026-08-25)

Importado desde ShiftFlow-sdaf-extract. Estado reseteado a **Pendiente** — reimplementar en este repo tras Gate 0 (specs Approved + ADRs Aceptado + worklog).

---

## Trasplante 2026-08-27

Código/tests verificados en este repo (worklogs/TRANSPLANTE/). DoD cubierto por build+tests; sin reescritura.
