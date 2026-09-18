# SPEC-PRD-004 — Capacidades `post-mvp-rules-v2`

| Campo | Valor |
|--------|--------|
| ID | SPEC-PRD-004 |
| Versión | 0.1.0 |
| Estado | Draft |
| Fecha | 2026-09-18 |
| Fuentes | Enmienda `post-mvp-rules-v2`, SPEC-PRD-001 (C-RUL), handbook H02/H04 |
| ADRs relacionados | ADR-009 |
| Backlog | PBI-016…022 |
| Derivados | SPEC-DOM-008, SPEC-APP-006, SPEC-ACC-006 |

---

## 1. Contexto

Capacidades de producto del corte post-MVP de reglas hard configurables. Complementa SPEC-PRD-001; no redefine el DoD de `mvp-0.1`.

---

## 2. Capacidades In

| ID | Capacidad | Notas |
|----|-----------|--------|
| C-RUL-CAT | Catálogo hard evaluable | Códigos HR-xx; mandatory vs optional |
| C-RUL-CFG | Configuración por Organization | Enable/params; UI mínima |
| C-RUL-HR04 | Hard noches pares/impares | Tras Approved ficha |
| C-RUL-HR05 | Hard bolsa mensual | |
| C-RUL-HR06 | Hard racha máxima | |
| C-RUL-HR07 | Hard cuotas nocturnas | |
| C-RUL-HR08 | Hard intermensual | Bloqueado hasta semántica DOCX |
| C-RUL-XPL | Explain stub ampliado | Sin mutación |

---

## 3. Out

Soft, Optimization, IA de escritura, UI avanzada de reglas, Compliance BC.

---

## 4. Criterios de aceptación de producto

1. Demo puede mostrar: desactivar HR-03 → asignar gap corto → OK; reactivar → rechazo.
2. Journey MVP sigue reproducible con defaults.
3. Al menos una hard nueva (HR-04 o HR-05) demostrable tras su PBI Approved+Hecho.

---

## 5. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-09-18 | Draft inicial |
