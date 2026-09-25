# SPEC-APP-006 — Configuración del catálogo y AssignShift v2 (hard + soft)

| Campo | Valor |
|--------|--------|
| ID | SPEC-APP-006 |
| Versión | 0.2.1 |
| Estado | Approved |
| Fecha | 2026-09-25T19:35+02:00 |
| Fuentes | SPEC-DOM-008, SPEC-APP-003, SPEC-APP-005, enmienda `post-mvp-rules-v2` |
| ADRs relacionados | ADR-003, ADR-009 |
| Backlog | PBI-016, PBI-017, PBI-023 (+ reglas 018…025) |
| Derivados | SPEC-ACC-006; API; UI mínima Blazor |

---

## 1. Contexto

Configurar el catálogo **hard y soft** por Organization y evolucionar `AssignShift` para consumir `RuleEvaluationResult`.

Actor: **Administrator**.

---

## 2. Configuración

| Operación | Postcondiciones | Errores |
|-----------|-----------------|---------|
| `GetOrganizationRuleConfig` | Lista hard+soft con Kind, Enabled, Mandatory, Params | 404; 401/403 |
| `UpsertOrganizationRuleConfig` | Persistido; no desactivar Mandatory; Kind inmutable respecto al catálogo | Params inválidos; Code desconocido; Kind mismatch |

UI mínima: enable/params para hard y soft (p. ej. HR-03 minutos; flags SR).

---

## 3. AssignShift (evolución)

1. Cargar contexto (ventana para reglas enabled).
2. Resolver hard activas (mandatory ∪ enabled) y soft enabled.
3. `RuleEngine.Evaluate(ctx)` → `RuleEvaluationResult`.
4. Si `HardViolations` no vacío: no persistir; 400 + explain del hard (lista o primera).
5. Si solo soft (o ninguno): **persistir**; respuesta de éxito incluye `warnings: [{ code, message, title?, body? }]` (puede estar vacío).
6. Soft no usa 400.

Compat: sin config → hard = SPEC-APP-003; `warnings` vacío.

---

## 4. Calendar (UI mínima — PBI-023)

Tras AssignShift exitoso con warnings: mostrar alerta no bloqueante (códigos SR-*) en Calendar. Hard sigue en error 400 como hoy.

---

## 5. Criterios de aceptación (aplicación)

1. Get/Upsert Administrator; no desactivar HR-01.
2. HR-03 off → gap corto persiste.
3. Soft enabled + condición → 2xx con `warnings` y asignación persistida.
4. Soft disabled → sin warnings en el mismo escenario.
5. Hard + soft a la vez → 400 hard; no persistir (soft no se evalúa o se ignora en respuesta de error; documentar: **no se exige** devolver soft si hay hard).

---

## 6. Out

- UI avanzada; Explain LLM; fairness avanzado; soft bloqueante.

---

## 7. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.2.1 | 2026-09-25T19:35+02:00 | Approved por @mortiz-iadev (CODEOWNERS), instrucción humana explícita «Aprueba la enmienda y las specs» (sdaf-core H13 §2) |
| 0.2.0 | 2026-09-18T23:06+02:00 | Soft warnings en éxito; UI avisos |
| 0.1.0 | 2026-09-18T22:47+02:00 | Draft solo hard |
