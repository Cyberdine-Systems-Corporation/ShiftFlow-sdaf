# SPEC-ACC-006 — Aceptación catálogo hard y configuración

| Campo | Valor |
|--------|--------|
| ID | SPEC-ACC-006 |
| Versión | 0.1.0 |
| Estado | Draft |
| Fecha | 2026-09-18 |
| Fuentes | SPEC-DOM-008, SPEC-APP-006, SPEC-DOM-006 |
| ADRs relacionados | ADR-009 |
| Backlog | PBI-016, PBI-017 |
| Derivados | IntegrationTests API (+ unit Domain catálogo) |

---

## 1. Contexto

Cubre la **plataforma** Rule Engine v2 (catálogo + config). No sustituye ACC-002/003/004 (regresión MVP) ni los ACC futuros por HR-04…08.

---

## 2. Escenarios

### ACC-R2-01 — Default equivale a v1 (HR-01)

**Dado** una Organization sin filas de `OrganizationRuleConfig`  
**Y** un empleado con turno Assigned solapado  
**Cuando** se intenta AssignShift solapado  
**Entonces** error `HR-01` y no se persiste

### ACC-R2-02 — No se puede desactivar HR-01

**Dado** Administrator autenticado  
**Cuando** Upsert config `HR-01` con `Enabled=false`  
**Entonces** la API rechaza la operación  
**Y** Evaluate sigue aplicando HR-01

### ACC-R2-03 — Desactivar HR-03

**Dado** Organization con HR-03 enabled y `MinimumRestMinutes=660` (rechazo conocido)  
**Cuando** se pone HR-03 `Enabled=false`  
**Y** se asigna un turno con gap &lt; 660  
**Entonces** la asignación **se persiste**

### ACC-R2-04 — Reactivar HR-03

**Dado** el estado tras ACC-R2-03  
**Cuando** se reactiva HR-03 con `MinimumRestMinutes=660`  
**Y** se intenta otro turno con gap insuficiente  
**Entonces** error `HR-03` y no se persiste

### ACC-R2-05 — Regresión journey MVP

**Dado** config default  
**Cuando** se ejecuta el journey SPEC-PRD-002 (API)  
**Entonces** HR-01/02/03 siguen comportándose como en mvp-0.1

---

## 3. Out

- Escenarios HR-04…08 (ACC dedicados por PBI).
- Soft rules.

---

## 4. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-09-18 | Draft inicial |
