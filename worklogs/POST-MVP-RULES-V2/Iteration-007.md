---
pbi: PBI-016
iteracion: Iteration-007
fecha: 2026-09-28
inicio: 2026-09-28T11:45:00+02:00
fin: 2026-09-28T12:25:00+02:00
agente: domain-application
modelo: claude-opus-5-5
version_prompt: "0.3"
prompt_base: domain-application-agent@0.3.0
prompts_adicionales: ninguno
skills: csharp-adr006-slice@0.3.0
reglas_ide: idioma-castellano, git-remoto-encargo, coding-standards-csharp
ad_hoc: "encargo del humano: «Si» a empezar el paso 4 del plan (corregir R3-null-rule-element y las regiones de RuleEngine); sin commit, rama ni PR (H06 §7)"
contexto: "Iteration-006 (testing-review-pr post-merge del PR 22) dejó dos menores en RuleEngine: R3-null-rule-element (RDD) y la falta de regiones conceptuales (QG-Docs, ADR-006 §3). R3-unknown-override-keys pasa a PBI-017."
especificaciones_utilizadas: specs/domain/SPEC-DOM-008-rule-engine-v2-catalog.md (§6.5), architecture/decisions/ADR-006-coding-standards.md (§3, §5, §6), architecture/decisions/ADR-009-rule-engine-v2-catalogo-hard.md, worklogs/POST-MVP-RULES-V2/Iteration-006.md
archivos_leidos: src/ShiftFlow.Domain/Rules/RuleEngine.cs, src/ShiftFlow.Domain/Leaves/Leave.cs, src/ShiftFlow.Domain/ShiftAssignments/ShiftAssignment.cs, tests/ShiftFlow.UnitTests/Domain/RuleEngineCatalogTests.cs
archivos_modificados: src/ShiftFlow.Domain/Rules/RuleEngine.cs, tests/ShiftFlow.UnitTests/Domain/RuleEngineCatalogTests.cs, worklogs/POST-MVP-RULES-V2/Iteration-007.md
origen_cambios: "ver tabla Origen de cambios"
resultado: "RuleEngine rechaza con ArgumentException una regla nula o sin código (nulo o en blanco) en el catálogo explícito (antes, NullReferenceException o un fallo tardío en IsEnabled) y queda organizado en regiones Factory, Behavior e Invariants como los agregados del dominio. 8 tests nuevos de las guardas. Sin cambio de comportamiento con el catálogo por defecto."
tiempo: PT0H40M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "Gate 0: corrección dentro del alcance de PBI-016 (Gate 0 PASS en Iteration-005; SPEC-DOM-008 y ADR-009 sin cambios). Las guardas de colecciones nulas y del contexto ya existían; ahora tienen test. Las validaciones del catálogo pasan a métodos EnsureNoNullRules y EnsureValidCodes en la región Invariants, con el patrón Ensure* de Leave y ShiftAssignment. Sin null-forgiving (!) en src/; en el test, los null! simulan un caller sin nullable habilitado y llevan comentario de justificación (playbook coding-standards-csharp, norma 4). Verificación de que los tests cubren el hallazgo: con el RuleEngine anterior, 2 de los 17 tests de RuleEngineCatalogTests fallan (Rechaza_hard_rule_nula_en_el_catalogo y Rechaza_soft_rule_nula_en_el_catalogo, por NullReferenceException); con el nuevo pasan todos. RDD sobre el cambio sin commit: review-493b090b35fd7985 (riesgo medium, consentimiento granted por el humano, lente review-reliability), aprobada y confirmada con 1 SUGGESTION, R3-null-rule-code (RuleEngine.cs:115-125): una regla no nula con Code nulo pasaba el HashSet y fallaba después en IsEnabled; venía del código base. Corregida por decisión humana en esta misma iteración: EnsureValidCodes rechaza códigos nulos o en blanco, con el test Rechaza_regla_sin_codigo (3 casos), que falla con la versión anterior y pasa con la nueva."
pruebas_ejecutadas: "dotnet build ShiftFlow.sln (0 avisos, 0 errores; CS1591 como error); dotnet test tests/ShiftFlow.UnitTests (62/62; antes 54); dotnet test tests/ShiftFlow.IntegrationTests (35/35); dotnet test --filter RuleEngineCatalogTests con el RuleEngine anterior (2 fallos esperados de 17) y restaurado el nuevo (59/59); dotnet test --filter Rechaza_regla_sin_codigo sin EnsureValidCodes (3 fallos esperados de 3) y con él (62/62); gentle-ai review status/start/capture-result/acknowledge-approved verbatim (review-493b090b35fd7985); validate-worklog.py sobre Iteration-006 e Iteration-007"
estado: hecho
siguiente_agente: "humano (ordenar commit/PR con Iteration-006 e Iteration-007; review RDD del rango antes del PR) y después testing-review (testing-review-pr antes del merge)"
commit: null
pr: null
rama: fix/pbi-016-rule-engine-guards
sha: null
resumen_acumulado: "domain-application-agent@0.3.0 + csharp-adr006-slice@0.3.0 — R3-null-rule-element y R3-null-rule-code corregidos y regiones en RuleEngine (menores 1 y 3 de Iteration-006); rama fix/pbi-016-rule-engine-guards; commit, PR y SHA ausentes"
---

# PBI-016 / Iteration-007

## Línea de decisión

- Menor 1 (R3-null-rule-element): `ArgumentException` con el nombre del parámetro, coherente con el rechazo de códigos duplicados (SPEC-DOM-008 §6.5) y con la documentación `<exception>` (ADR-006 §5).
- Menor 3 (QG-Docs): regiones `Factory`, `Behavior` e `Invariants`, las mismas que usan `Leave` y `ShiftAssignment` (ADR-006 §3).
- Menor 2 (R3-unknown-override-keys) no se toca: sin impacto hoy y los overrides persistidos llegan en PBI-017, donde se valida contra el catálogo.
- Tests con los nombres del comportamiento esperado, en `RuleEngineCatalogTests` (ADR-006; playbook norma 6).

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| `src/ShiftFlow.Domain/Rules/RuleEngine.cs` | Iteration-006 menores 1 y 3; SPEC-DOM-008 §6.5; ADR-006 §3 y §5 | Guardas de reglas nulas y de códigos nulos o en blanco; regiones; `<exception>` completas |
| `tests/ShiftFlow.UnitTests/Domain/RuleEngineCatalogTests.cs` | Iteration-006 menor 1; R3-null-rule-code; H09 | 8 casos: colecciones nulas, elementos nulos, contexto nulo y código nulo o en blanco |
