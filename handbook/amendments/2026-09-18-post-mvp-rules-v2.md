# Enmienda — corte `post-mvp-rules-v2`

| Campo | Valor |
|--------|--------|
| **Estado** | Draft (pendiente aprobación humana) |
| **Fecha** | 2026-09-18 |
| **Norma superior** | [03-mvp-definition.md](../03-mvp-definition.md), [04-product-roadmap.md](../04-product-roadmap.md), [02-product-vision.md](../02-product-vision.md) |
| **ADR** | [ADR-009](../../architecture/decisions/ADR-009-rule-engine-v2-catalogo-hard.md) (Propuesto) |
| **Specs** | SPEC-PRD-004, SPEC-DOM-008, SPEC-APP-006, SPEC-ACC-006 (Draft) |

---

## 1. Motivación

El MVP `mvp-0.1` está cerrado (backlog PBI-001…015 Hecho; paridad extract firmada). El roadmap post-MVP prioriza ampliar el Rule Engine. Esta enmienda fija el **primer corte post-MVP** centrado en **hard rules configurables**, sin abrir soft ni motores Optimization/Compliance separados.

---

## 2. Nombre del corte

`post-mvp-rules-v2`

---

## 3. Incluido (In)

| Tema | Detalle |
|------|---------|
| Catálogo hard | Reglas `IHardRule` con códigos estables `HR-xx`; evaluación antes de persistir `AssignShift` |
| Config por Organization | Enable/disable + parámetros; **HR-01 (solape) siempre activa** (no desactivable) |
| Migración v1 | HR-02 y HR-03 pasan al catálogo; comportamiento default = MVP |
| Hard nuevas | HR-04…HR-08 según [knowledge/curated/hard-rules-catalog.md](../../knowledge/curated/hard-rules-catalog.md) y SPEC-DOM-008 |
| UI | **Mínima**: pantalla/sección admin de reglas de la organización (enable + params). Sin rediseño UX global |
| Explain | Extender stub de explicación a códigos nuevos (sin mutar cuadrante) |
| Tests | Unit Domain + ACC API por regla / config |

---

## 4. Excluido (Out) en este corte

- Soft preferences / fairness scoring.
- Optimization Engine; IA que genere o persista cuadrantes.
- Compliance Engine como BC o servicio aparte.
- Motor de reglas externo (Drools, DSL).
- MAUI, SignalR, multitenancy, ERP/AD.
- UI avanzada de catálogo (matriz masiva, preview de impacto, historial de cambios de config).

---

## 5. Relación con el MVP

- No invalida el DoD de `mvp-0.1`.
- El journey demo MVP debe seguir verde con la **config por defecto** (equivalente a Rule Engine v1).
- ADR-003 sigue vigente para el MVP; ADR-009 lo **extiende** para post-MVP sin deprecar las tres hard rules.

---

## 6. Criterios de aceptación de la enmienda

1. Existe ADR-009 Propuesto/Aceptado alineado a este corte.
2. Specs Draft del catálogo enlazan PBIs 016–022.
3. Soft y Optimization permanecen Out de forma explícita.
4. Tras aprobación humana: actualizar estado de esta enmienda y, si aplica, historial de H02/H03/H04.

---

## 7. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-09-18 | Draft inicial Fase 0 (Specification) |
