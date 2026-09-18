# ADR-009 — Rule Engine v2: catálogo hard configurable

| Campo | Valor |
|--------|--------|
| Estado | Propuesto |
| Fecha | 2026-09-18 |
| Decisores | Architecture / Director técnico (pendiente aceptación humana) |
| Relacionado | ADR-003, `handbook/amendments/2026-09-18-post-mvp-rules-v2.md`, SPEC-DOM-008, PBI-016…022 |

---

## Contexto

ADR-003 fijó Rule Engine v1 (≤3 hard fijas dentro del BC) para el MVP. El MVP está cerrado. El roadmap pide ampliar reglas desde knowledge sin abrir cinco motores.

Hace falta decidir cómo crecer: seguir apilando `if` en una clase, externalizar un motor, o un **catálogo interno configurable**.

---

## Decisión

1. **Rule Engine v2** = catálogo de `IHardRule` + `RuleEngine` orquestador **dentro** del BC WorkforceScheduling (no microservicio, no BC Compliance).
2. **Configuración** por Organization (`OrganizationRuleConfig`: Enabled + params). Persistencia EF; params JSON versionado por `Code` o columnas equivalentes validadas en Application.
3. **HR-01** es mandatory (siempre on). HR-02/03 y nuevas hard son opt-in vía config (default = on / paridad MVP).
4. **Soft rules, Optimization, Drools/DSL externo**: Out de este ADR.
5. **UI**: mínima (admin enable/params). Sin redesign.
6. **Explain**: sigue siendo puerto de infraestructura (stub); solo se amplían plantillas de códigos.
7. Separar “Compliance Engine” solo con **nuevo ADR** si el catálogo supera un umbral de complejidad acordado en retrospectiva (no por defecto).

ADR-003 permanece válido para el alcance MVP; este ADR lo **extiende** para `post-mvp-rules-v2`.

---

## Alternativas consideradas

| Alternativa | Motivo de rechazo (ahora) |
|-------------|---------------------------|
| Seguir con clase monolítica + más métodos | Frágil; difícil config/enable por org |
| Drools / motor externo | Infra y ops injustificados |
| Compliance BC / servicio | Sobre-diseño; handbook 11 |
| Soft + hard en el mismo corte | Diluye DoD; soft Out explícito |

---

## Consecuencias

### Positivas

- Extensión por PBI/regla sin reescribir AssignShift.
- Config demostrable; regresión MVP por defaults.
- Trazabilidad código `HR-xx` estable.

### Negativas / costes

- Migración de `MinimumRestMinutes` hacia params de HR-03 (dual-read temporal aceptable).
- Semántica HR-04…08 exige curación DOCX antes de Approved/Gate 0 de esos PBI.
- AssignShift debe cargar contexto más amplio (ventanas) con cuidado de rendimiento.

### Cumplimiento

- Gate 0: specs Approved + este ADR Aceptado antes de implementar PBI-016+.
- Tests ACC-006 + regresión journey.

---

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-09-18 | Propuesto (Fase 0) |
