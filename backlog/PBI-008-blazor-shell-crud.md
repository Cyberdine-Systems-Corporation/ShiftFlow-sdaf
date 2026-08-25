# PBI-008 — Blazor shell + CRUD maestros

| Campo | Valor |
|--------|--------|
| Sprint | 1–2 |
| Prioridad | 8 |
| Specs | SPEC-PRD-002 (AC-01), SPEC-APP-001 |
| DoD | Nav + pantallas para org/dept/employee/shift type/calendario |
| Estado | Pendiente |

## Descripción

UI Web demostrable sobre las capacidades de maestros y planificación.

## Notas de implementación

- Login + nav Administrator → Organizaciones / Calendario / Ausencias (calendario real: PBI-005; jerarquía: PBI-015).
- CRUD maestros en `/organizations` y `/organizations/{id}` vía `MastersApiClient`.

---

## Importación (2026-08-25)

Importado desde ShiftFlow-sdaf-extract. Estado reseteado a **Pendiente** — reimplementar en este repo tras Gate 0 (specs Approved + ADRs Aceptado + worklog).