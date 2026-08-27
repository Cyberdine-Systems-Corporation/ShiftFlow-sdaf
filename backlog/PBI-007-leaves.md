# PBI-007 — Leaves / ausencias

| Campo | Valor |
|--------|--------|
| Sprint | 2 |
| Prioridad | 7 |
| Specs | [SPEC-DOM-007](../specs/domain/SPEC-DOM-007-leave.md), [SPEC-APP-004](../specs/application/SPEC-APP-004-leave-use-cases.md), [SPEC-ACC-003](../specs/acceptance/SPEC-ACC-003-leave-and-hr02.md) **Draft**; SPEC-PRD-001/002 (C-LEA, AC-04), SPEC-DOM-006 (HR-02) |
| DoD | Registrar/cancelar leave; ListLeaves; HR-02 bloquea AssignShift; rechazo observable; UI + proyección calendario |
| Estado | Hecho |

## Descripción

Gestión básica de vacaciones/ausencias que bloquean turnos.  
Sin workflow de aprobación (Approve/Reject del knowledge = Out MVP).

## Notas

- HR-03 (descanso mínimo) → PBI-006.
- Volumen Postgres existente puede requerir reset tras migraciones (ADR-007).

---

## Importación (2026-08-25)

Importado desde ShiftFlow-sdaf-extract. Estado reseteado a **Pendiente** — reimplementar en este repo tras Gate 0 (specs Approved + ADRs Aceptado + worklog).

---

## Trasplante 2026-08-27

Código/tests verificados en este repo (worklogs/TRANSPLANTE/). DoD cubierto por build+tests; sin reescritura.
