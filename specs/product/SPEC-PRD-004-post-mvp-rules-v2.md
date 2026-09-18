# SPEC-PRD-004 — Capacidades `post-mvp-rules-v2`

| Campo | Valor |
|--------|--------|
| ID | SPEC-PRD-004 |
| Versión | 0.2.0 |
| Estado | Draft |
| Fecha | 2026-09-18 |
| Fuentes | Enmienda `post-mvp-rules-v2`, SPEC-PRD-001 (C-RUL), handbook H02/H04 |
| ADRs relacionados | ADR-009 |
| Backlog | PBI-016…025 |
| Derivados | SPEC-DOM-008, SPEC-APP-006, SPEC-ACC-006 |

---

## 1. Contexto

Capacidades del corte post-MVP de reglas **hard y soft** configurables. Complementa SPEC-PRD-001; no redefine el DoD de `mvp-0.1`.

---

## 2. Capacidades In

| ID | Capacidad | Notas |
|----|-----------|--------|
| C-RUL-CAT | Catálogo hard + soft | `HR-xx` bloquean; `SR-xx` avisan |
| C-RUL-CFG | Configuración por Organization | Enable/params; UI mínima |
| C-RUL-WARN | Avisos soft en AssignShift/Calendar | Persistencia OK + warnings |
| C-RUL-HR04…08 | Hard nuevas | Según PBIs; HR-08 bloqueado semántica |
| C-RUL-SR01 | Soft preferencia fin de semana | PBI-024 |
| C-RUL-SR02 | Soft preferencia tipología | PBI-025 |
| C-RUL-XPL | Explain stub ampliado | HR y SR; sin mutación |

---

## 3. Out

Fairness scoring avanzado, Optimization, IA de escritura, UI avanzada, Compliance BC.

---

## 4. Criterios de aceptación de producto

1. Demo: desactivar HR-03 → gap corto OK; reactivar → rechazo.
2. Demo: soft enabled → asignación OK con aviso visible.
3. Journey MVP reproducible con defaults (sin soft obligatorio).
4. Al menos una hard nueva y un soft piloto demostrables tras sus PBI.

---

## 5. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.2.0 | 2026-09-18 | Soft In (SR-01/02, warnings) |
| 0.1.0 | 2026-09-18 | Draft solo hard |
