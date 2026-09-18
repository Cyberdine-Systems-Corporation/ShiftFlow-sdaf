# SPEC-ACC-006 — Aceptación catálogo hard/soft y configuración

| Campo | Valor |
|--------|--------|
| ID | SPEC-ACC-006 |
| Versión | 0.2.0 |
| Estado | Draft |
| Fecha | 2026-09-18 |
| Fuentes | SPEC-DOM-008, SPEC-APP-006, SPEC-DOM-006 |
| ADRs relacionados | ADR-009 |
| Backlog | PBI-016, PBI-017, PBI-023 |
| Derivados | IntegrationTests API (+ unit Domain) |

---

## 1. Contexto

Plataforma Rule Engine v2 (catálogo hard+soft + config + avisos). No sustituye ACC-002/003/004 ni ACC por HR-04…08 / SR-01/02 detallados.

---

## 2. Escenarios hard / config

### ACC-R2-01 — Default equivale a v1 (HR-01)

**Dado** Organization sin filas de config  
**Cuando** AssignShift solapado  
**Entonces** error `HR-01` y no se persiste

### ACC-R2-02 — No se puede desactivar HR-01

**Cuando** Upsert `HR-01` Enabled=false  
**Entonces** rechazo de la operación y HR-01 sigue activa

### ACC-R2-03 — Desactivar HR-03

**Cuando** HR-03 Enabled=false y AssignShift con gap &lt; umbral previo  
**Entonces** se persiste

### ACC-R2-04 — Reactivar HR-03

**Cuando** HR-03 enabled con umbral y gap insuficiente  
**Entonces** error `HR-03` y no se persiste

### ACC-R2-05 — Regresión journey MVP

**Dado** config default  
**Cuando** journey SPEC-PRD-002  
**Entonces** HR-01/02/03 como mvp-0.1 y sin warnings soft obligatorios

---

## 3. Escenarios soft (plataforma)

### ACC-R2-S01 — Soft no bloquea

**Dado** SR-01 (o soft de prueba) enabled y condición de aviso cumplida  
**Y** sin violación hard  
**Cuando** AssignShift  
**Entonces** HTTP éxito y cuerpo con `warnings` que incluye el código soft  
**Y** la asignación está persistida

### ACC-R2-S02 — Soft disabled

**Dado** el mismo escenario con soft Enabled=false  
**Cuando** AssignShift  
**Entonces** éxito sin ese código en `warnings`

### ACC-R2-S03 — Hard gana a soft

**Dado** soft enabled y hard HR-01 incumplida  
**Cuando** AssignShift  
**Entonces** 400 `HR-01` y no se persiste

---

## 4. Out

- Detalle semántico HR-04…08 / SR-01/02 (ACC por PBI).
- Fairness avanzado.

---

## 5. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.2.0 | 2026-09-18 | Escenarios soft ACC-R2-S* |
| 0.1.0 | 2026-09-18 | Draft solo hard |
