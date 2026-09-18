# SPEC-DOM-008 — Rule Engine v2 (catálogo hard configurable)

| Campo | Valor |
|--------|--------|
| ID | SPEC-DOM-008 |
| Versión | 0.1.0 |
| Estado | Draft |
| Fecha | 2026-09-18 |
| Fuentes | SPEC-DOM-006 (Approved), ADR-003, ADR-009 (Propuesto), `handbook/amendments/2026-09-18-post-mvp-rules-v2.md`, `knowledge/curated/hard-rules-catalog.md` |
| ADRs relacionados | ADR-003, ADR-009 |
| Backlog | PBI-016…PBI-022 |
| Derivados | SPEC-APP-006, SPEC-ACC-006, anexos por HR en §4 |

---

## 1. Contexto

Evoluciona el Rule Engine v1 (tres hard fijas en una clase) a un **catálogo de hard rules** evaluadas dentro del BC WorkforceScheduling.

Contrato:

```text
Evaluate(RuleEvaluationContext) → ok | RuleViolation[]
```

`AssignShift` invoca la evaluación **antes** de persistir, con el conjunto de reglas **activas** de la Organization.

**No** introduce soft rules ni un BC Compliance aparte.

---

## 2. Alcance

### 2.1 In

- Interfaz de dominio `IHardRule` (`Code`, `IsMandatory`, `Evaluate(ctx)`).
- `RuleEvaluationContext`: candidato, Assigned relevantes, Leaves activos, Organization, parámetros por código.
- `RuleEngine` orquesta la lista activa y acumula `RuleViolation`.
- `OrganizationRuleConfig`: por `(OrganizationId, Code)` → `Enabled` + params tipados/JSON versionado.
- HR-01 **mandatory** (siempre evaluada; no desactivable vía config).
- HR-02, HR-03 migradas al catálogo; default = comportamiento SPEC-DOM-006.
- Códigos nuevos HR-04…HR-08 según §4 (semántica provisional hasta curación DOCX / Approved).

### 2.2 Out

- Soft / fairness; Optimization; DSL externo; Compliance Engine separado.
- Desactivar HR-01.
- Semántica definitiva de HR-08 sin ficha DOCX aprobada.

---

## 3. Modelo de configuración

| Concepto | Regla |
|----------|--------|
| Ausencia de fila de config | Tratar como **enabled** con defaults de producto (paridad MVP) |
| `Enabled=false` | No evaluar (salvo `IsMandatory`) |
| Params inválidos | Rechazo de comando de configuración (Application); no silenciar en Evaluate |
| Varias violaciones | `Evaluate` puede devolver N; al menos una basta para no persistir |

---

## 4. Catálogo hard

### 4.1 MVP (normativa vigente SPEC-DOM-006)

| Código | Mandatory | Params | Spec detalle |
|--------|-----------|--------|--------------|
| HR-01 | Sí | — | SPEC-DOM-006 §2.1 |
| HR-02 | No | — | SPEC-DOM-006 §2.2 / DOM-007 |
| HR-03 | No | `MinimumRestMinutes` | SPEC-DOM-006 §2.3 |

### 4.2 Post-MVP (provisional — knowledge curated)

| Código | Nombre | PBI | Nota |
|--------|--------|-----|------|
| HR-04 | Noches pares/impares | PBI-018 | Draft; validar DOCX |
| HR-05 | Bolsa mensual horas | PBI-019 | Draft |
| HR-06 | Racha máxima días | PBI-020 | Draft; “ocho días” |
| HR-07 | Cuotas nocturnas | PBI-021 | Draft; reutiliza noche HR-04 |
| HR-08 | Intermensual | PBI-022 | **Bloqueado** hasta semántica cerrada |

Detalle operativo: [`knowledge/curated/hard-rules-catalog.md`](../../knowledge/curated/hard-rules-catalog.md).

---

## 5. Criterios de aceptación (dominio)

1. Con config default, Evaluate equivale a v1 (HR-01/02/03).
2. HR-01 no se omite aunque exista config `Enabled=false`.
3. Regla no enabled (no mandatory) no aporta violaciones.
4. Contexto de evaluación no provoca N+1 implícito: el caller carga datos de ventana una vez.
5. Códigos de violación estables y observables en API (`HR-xx`).

---

## 6. Fuera de alcance

Igual que enmienda `post-mvp-rules-v2` §4 (soft, Optimization, UI avanzada, IA que mute).

---

## 7. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-09-18 | Draft inicial Fase 0 |
