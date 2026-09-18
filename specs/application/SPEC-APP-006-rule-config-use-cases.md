# SPEC-APP-006 — Configuración del catálogo de reglas y AssignShift v2

| Campo | Valor |
|--------|--------|
| ID | SPEC-APP-006 |
| Versión | 0.1.0 |
| Estado | Draft |
| Fecha | 2026-09-18 |
| Fuentes | SPEC-DOM-008, SPEC-APP-003, SPEC-APP-005, enmienda `post-mvp-rules-v2` |
| ADRs relacionados | ADR-003, ADR-009 |
| Backlog | PBI-016, PBI-017 (+ explain en PBI-018…) |
| Derivados | SPEC-ACC-006; endpoints API; UI mínima Blazor |

---

## 1. Contexto

Casos de uso para **consultar y actualizar** la configuración hard por Organization, y evolución de `AssignShift` para consumir el catálogo activo.

Actor: **Administrator** autenticado.

---

## 2. Comandos / consultas de configuración

| Operación | Precondiciones | Postcondiciones | Errores |
|-----------|----------------|-----------------|---------|
| `GetOrganizationRuleConfig` | Org existe; Actor Administrator | Lista de reglas del catálogo con Enabled, Mandatory, Params efectivos | 404 org; 401/403 |
| `UpsertOrganizationRuleConfig` | Org existe; Code conocido; si Mandatory, no permitir Enabled=false | Config persistida; Evaluate posterior respeta cambios | Params inválidos; intento de desactivar Mandatory; Code desconocido |

UI mínima (In): una sección/pantalla Blazor bajo maestros/org que invoca estas operaciones (enable + editar params visibles, p. ej. `MinimumRestMinutes` de HR-03).

---

## 3. AssignShift (evolución)

Respecto a SPEC-APP-003:

1. Cargar Assigned/Leaves/params necesarios en `RuleEvaluationContext` (ventana suficiente para reglas enabled).
2. Resolver reglas activas (mandatory ∪ enabled).
3. `RuleEngine.Evaluate(ctx)`.
4. Si hay violaciones: no persistir; construir `RuleViolationException` (como mínimo la primera; preferible exponer lista/códigos en cuerpo API en el mismo corte de plataforma o en follow-up documentado).
5. Invocar `IRuleExplanation` para el código rechazado (stub; sin mutar).

Compat: sin filas de config → comportamiento idéntico a SPEC-APP-003 / DOM-006.

---

## 4. Criterios de aceptación (aplicación)

1. Get/Upsert solo Administrator.
2. Upsert no puede desactivar HR-01.
3. Tras desactivar HR-03, AssignShift con gap corto **persiste** (ACC).
4. Tras reactivar HR-03 con umbral &gt; gap, **rechaza** con `HR-03`.

---

## 5. Out

- UI avanzada (preview masivo, historial de auditoría de config).
- Soft rules.
- Explain con LLM.

---

## 6. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-09-18 | Draft inicial |
