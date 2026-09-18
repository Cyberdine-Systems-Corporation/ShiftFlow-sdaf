# Catálogo curado — hard rules (post-MVP)

| Campo | Valor |
|--------|--------|
| Fuente raw | `knowledge/raw/Domain-Specs-V1.docx`, `2026-07-ShiftFlow-AI-Engineering-Master-Prompt.md`, `handbook/03-mvp-definition.md` §4.3 |
| Estado | Draft curated — **semántica de HR-04…08 provisional** hasta validación humana contra el DOCX |
| Fecha | 2026-09-18 |
| Soft | Out de este catálogo (no fichas soft aquí) |

Extracción append-only. No sustituye specs; alimenta SPEC-DOM-008 y PBIs.

---

## Ya en MVP (Rule Engine v1)

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
| Params | `Parity` = `Even` \| `Odd`; definición de noche: `NightStartHour`/`NightEndHour` org **o** flag en ShiftType (decidir en spec) |
| Dependencias | Employee u Organization policy; catálogo ShiftType |
| Riesgo | Definición de “noche” y zona horaria (MVP homogéneo) |
| PBI | PBI-018 |

### HR-05 — Bolsa mensual de horas

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | Horas `Assigned` del empleado en el mes calendario de `StartAt` + duración del candidato &gt; `MaxMonthlyHours` |
| Params | `MaxMonthlyHours` (decimal/horas) |
| Dependencias | Suma de intervalos Assigned del mes |
| PBI | PBI-019 |

### HR-06 — Racha máxima (“ocho días”)

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | Tras asignar el candidato, el empleado tendría más de `MaxConsecutiveWorkDays` días calendario consecutivos con ≥1 turno Assigned |
| Params | `MaxConsecutiveWorkDays` (entero; knowledge “ocho” → default documentado 8) |
| Dependencias | Assigned del empleado en ventana alrededor del candidato |
| PBI | PBI-020 |

### HR-07 — Cuotas nocturnas

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | Nº de turnos noche Assigned en la ventana (mes o rolling) sale de `[MinNights, MaxNights]` al incluir el candidato |
| Params | `MinNights`, `MaxNights`, `Window` = `CalendarMonth` \| `RollingDays` (+ `RollingDayCount` si aplica) |
| Dependencias | Misma definición de noche que HR-04 |
| PBI | PBI-021 |

### HR-08 — Validación intermensual

| Campo | Valor provisional |
|--------|-------------------|
| Bloquea si | La asignación cruza o incumple una restricción que involucra el mes anterior/siguiente (p. ej. descanso mínimo **entre** el último Assigned del mes M−1 y el primero de M, o tope de bolsa que arrastra — **fijar una sola semántica en aprobación de spec**) |
| Params | Pendiente de ficha DOCX; placeholder `Mode` hasta curación fina |
| Dependencias | Assigned en frontera de mes; posible interacción HR-03/HR-05 |
| PBI | PBI-022 |
| Nota | **No implementar** hasta cerrar semántica con humano + DOCX |

---

## Out explícito (no curar como hard en v2)

- Preferencias blandas / fairness scoring.
- Contratos parciales como motor aparte (puede informar params futuros).
- Alertas no bloqueantes.
- Optimization / auto-scheduling.

---

## Historial

| Fecha | Cambio |
|--------|--------|
| 2026-09-18 | Primera curación Draft desde handbook + master prompt (sin parseo DOCX binario) |
