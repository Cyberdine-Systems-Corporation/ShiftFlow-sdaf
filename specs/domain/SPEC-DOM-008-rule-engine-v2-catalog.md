# SPEC-DOM-008 — Rule Engine v2 (catálogo hard + soft configurable)

| Campo | Valor |
|--------|--------|
| ID | SPEC-DOM-008 |
| Versión | 0.2.0 |
| Estado | Draft |
| Fecha | 2026-09-18 |
| Fuentes | SPEC-DOM-006 (Approved), ADR-003, ADR-009 (Propuesto), enmienda `post-mvp-rules-v2`, `knowledge/curated/hard-rules-catalog.md` |
| ADRs relacionados | ADR-003, ADR-009 |
| Backlog | PBI-016…PBI-025 |
| Derivados | SPEC-APP-006, SPEC-ACC-006 |

---

## 1. Contexto

Evoluciona el Rule Engine v1 a un **catálogo configurable** de reglas **hard** (bloquean) y **soft** (avisan) dentro del BC WorkforceScheduling.

Contrato:

```text
Evaluate(RuleEvaluationContext) → RuleEvaluationResult
  HardViolations: RuleViolation[]   // bloquean persistencia si Count > 0
  SoftWarnings: RuleWarning[]       // no bloquean; Code SR-xx + Message
```

`AssignShift` evalúa **antes** de decidir persistir.

**No** introduce BC Compliance ni Optimization.

---

## 2. Alcance

### 2.1 In

- `IHardRule` (`Code`, `IsMandatory`, `Evaluate` → `RuleViolation?`).
- `ISoftRule` (`Code`, `Evaluate` → `RuleWarning?`); soft **nunca** `IsMandatory` de bloqueo.
- `RuleEvaluationContext` compartido.
- `RuleEngine` orquesta hard activas y soft activas.
- `OrganizationRuleConfig`: `(OrganizationId, Code)` → `Kind` (Hard\|Soft), `Enabled`, Params.
- HR-01 mandatory; HR-02/03 migradas; HR-04…08 según §4.
- Soft piloto SR-01, SR-02 según §5.

### 2.2 Out

- Fairness scoring avanzado / ranking global.
- Optimization; DSL externo; Compliance BC.
- Soft que impida persistir.
- Desactivar HR-01.
- Semántica definitiva HR-08 / SR sin ficha DOCX cuando esté bloqueada.

---

## 3. Modelo de configuración

| Concepto | Regla |
|----------|--------|
| Ausencia de fila | Hard MVP: enabled (paridad v1). Soft piloto: **disabled** por defecto |
| `Enabled=false` | No evaluar (salvo hard mandatory) |
| Kind | Debe coincidir con el registro del catálogo (no reclasificar HR↔SR por config) |
| Params inválidos | Rechazo en comando de configuración |
| HardViolations | Cualquiera ⇒ no persistir |
| SoftWarnings | Informativos; AssignShift puede persistir |

---

## 4. Catálogo hard

### 4.1 MVP (SPEC-DOM-006)

| Código | Mandatory | Params |
|--------|-----------|--------|
| HR-01 | Sí | — |
| HR-02 | No | — |
| HR-03 | No | `MinimumRestMinutes` |

### 4.2 Post-MVP (provisional)

| Código | Nombre | PBI |
|--------|--------|-----|
| HR-04 | Noches pares/impares | PBI-018 |
| HR-05 | Bolsa mensual horas | PBI-019 |
| HR-06 | Racha máxima días | PBI-020 |
| HR-07 | Cuotas nocturnas | PBI-021 |
| HR-08 | Intermensual | PBI-022 (bloqueado semántica) |

Detalle: [`knowledge/curated/hard-rules-catalog.md`](../../knowledge/curated/hard-rules-catalog.md).

---

## 5. Catálogo soft (piloto)

| Código | Nombre | Avisa si | Params | PBI |
|--------|--------|----------|--------|-----|
| SR-01 | Preferencia fin de semana | El candidato cae en sáb/dom y la policy del empleado/org marca preferencia de libre | `PreferFreeWeekend` bool (empleado u org) | PBI-024 |
| SR-02 | Preferencia de tipología | El `ShiftType` del candidato no está en la lista preferida del empleado (si la lista no vacía) | `PreferredShiftTypeIds[]` | PBI-025 |

Semántica provisional hasta validación DOCX. Fairness scoring **no** es SR en este corte.

---

## 6. Criterios de aceptación (dominio)

1. Config default hard ≡ v1; soft no aporta warnings si disabled.
2. HR-01 no se omite.
3. Soft enabled puede añadir `SoftWarnings` sin impedir ok de hard.
4. Caller carga ventana una vez en el contexto.
5. Códigos estables `HR-xx` / `SR-xx`.

---

## 7. Fuera de alcance

Enmienda `post-mvp-rules-v2` §4.

---

## 8. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.2.0 | 2026-09-18 | Soft piloto SR-01/02 + `RuleEvaluationResult` |
| 0.1.0 | 2026-09-18 | Draft solo hard |
