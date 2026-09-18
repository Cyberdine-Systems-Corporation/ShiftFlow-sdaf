# Catálogo curado — hard y soft rules (post-MVP)

| Campo | Valor |
|--------|--------|
| Fuente raw | `knowledge/raw/Domain-Specs-V1.docx`, `2026-07-ShiftFlow-AI-Engineering-Master-Prompt.md`, `handbook/03-mvp-definition.md` §4.3 |
| Estado | Draft curated — semántica HR-04…08 y SR-* **provisional** hasta validación humana contra el DOCX |
| Fecha | 2026-09-18 |
| Soft | Piloto SR-01/SR-02 In; fairness scoring avanzado Out |

Extracción append-only. No sustituye specs; alimenta SPEC-DOM-008 y PBIs.

---

## Ya en MVP (Rule Engine v1) — hard

| Código | Nombre | Params | Mandatory |
|--------|--------|--------|-----------|
| HR-01 | No solape misma persona | — | **Sí** (siempre on) |
| HR-02 | Leave activo bloquea | — | No (default on) |
| HR-03 | Descanso mínimo | `MinimumRestMinutes` (≥ 0) | No (default on; 0 = no aplica) |

---

## Candidatas hard (oleada post-MVP)

### HR-04 — Noches pares / impares

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | El candidato es turno “noche” y el día calendario de inicio no cumple la paridad requerida del empleado/org |
| Params | `Parity` = `Even` \| `Odd`; definición de noche: `NightStartHour`/`NightEndHour` org **o** flag en ShiftType |
| PBI | PBI-018 |

### HR-05 — Bolsa mensual de horas

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | Horas Assigned del mes + candidato &gt; `MaxMonthlyHours` |
| Params | `MaxMonthlyHours` |
| PBI | PBI-019 |

### HR-06 — Racha máxima (“ocho días”)

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | Más de `MaxConsecutiveWorkDays` días consecutivos con Assigned |
| Params | `MaxConsecutiveWorkDays` (default documentado 8) |
| PBI | PBI-020 |

### HR-07 — Cuotas nocturnas

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | Noches en ventana fuera de `[MinNights, MaxNights]` |
| Params | `MinNights`, `MaxNights`, `Window` |
| PBI | PBI-021 |

### HR-08 — Validación intermensual

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | Incumple restricción de frontera de mes (semántica a fijar con DOCX) |
| PBI | PBI-022 |
| Nota | **No implementar** hasta cerrar semántica |

---

## Soft piloto (In del corte)

### SR-01 — Preferencia fin de semana

| Campo | Valor provisional |
|--------|-------------------|
| Severidad | Soft (aviso; **no** bloquea) |
| Avisa si | Candidato en sáb/dom y preferencia de libre en fin de semana |
| Params | `PreferFreeWeekend` (bool) |
| Default config | Disabled |
| PBI | PBI-024 |

### SR-02 — Preferencia de tipología de turno

| Campo | Valor provisional |
|--------|-------------------|
| Severidad | Soft |
| Avisa si | Lista preferida no vacía y el ShiftType del candidato no está en ella |
| Params | `PreferredShiftTypeIds` |
| Default config | Disabled |
| PBI | PBI-025 |

---

## Out explícito (no en v2)

- Fairness scoring / ranking global.
- Soft adicionales del DOCX no listados (entrar por enmienda).
- Optimization / auto-scheduling.
- Soft bloqueante.

---

## Historial

| Fecha | Cambio |
|--------|--------|
| 2026-09-18 | Soft piloto SR-01/SR-02; fairness avanzado Out |
| 2026-09-18 | Primera curación Draft hard |
