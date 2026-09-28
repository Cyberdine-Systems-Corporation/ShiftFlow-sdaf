---
pbi: PBI-016
iteracion: Iteration-005
fecha: 2026-09-27
inicio: 2026-09-27T21:00:00+02:00
fin: 2026-09-27T21:12:00+02:00
agente: testing-review (Gate 0) + domain-application (implementación)
modelo: claude-opus-5-5
version_prompt: "0.3"
prompt_base: domain-application-agent@0.3.0
prompts_adicionales: testing-review-agent@0.3.2 (Gate 0)
skills: sdaf-gate0@0.2.0, csharp-adr006-slice@0.3.0, sdaf-worklog-handoff@0.4.0
reglas_ide: idioma-castellano, git-remoto-encargo, coding-standards-csharp
ad_hoc: "encargo del humano «Ejecuta siguiente PBI»; sin commit, rama ni PR (H06 §7)"
contexto: "Primer PBI del corte post-mvp-rules-v2 (Approved): catálogo IHardRule + ISoftRule sobre el RuleEngine v1, sin cambio observable en AssignShift."
especificaciones_utilizadas: backlog/PBI-016-rule-catalog-ihardrule.md, specs/domain/SPEC-DOM-008-rule-engine-v2-catalog.md, specs/domain/SPEC-DOM-006-rule-engine-v1.md, specs/application/SPEC-APP-006-rule-config-use-cases.md, specs/acceptance/SPEC-ACC-006-rule-catalog-config.md, architecture/decisions/ADR-009-rule-engine-v2-catalogo-hard.md, architecture/decisions/ADR-003-motores-planificacion-mvp.md
archivos_leidos: backlog/README.md, backlog/PBI-016-rule-catalog-ihardrule.md, handbook/04-product-roadmap.md, worklogs/POST-MVP-RULES-V2/Iteration-004.md, skills/sdaf-gate0/SKILL.md, skills/csharp-adr006-slice/SKILL.md, prompts/agents/domain-application-agent.md, src/ShiftFlow.Domain/Rules/RuleEngine.cs, src/ShiftFlow.Domain/Rules/RuleViolation.cs, src/ShiftFlow.Application/ShiftAssignments/AssignShift.cs, src/ShiftFlow.Application/Common/RuleViolationException.cs, tests/ShiftFlow.UnitTests/Domain/ShiftAssignmentAndRulesTests.cs, tests/ShiftFlow.UnitTests/Domain/LeaveAndHr02Tests.cs
archivos_modificados: src/ShiftFlow.Domain/Rules/ (IHardRule.cs, ISoftRule.cs, RuleWarning.cs, RuleEvaluationContext.cs, RuleEvaluationResult.cs, Hr01NoOverlapRule.cs, Hr02ActiveLeaveRule.cs, Hr03MinimumRestRule.cs, Sr01WeekendPreferenceRule.cs, Sr02ShiftTypePreferenceRule.cs, RuleCatalog.cs, ShiftIntervals.cs, RuleEngine.cs), src/ShiftFlow.Application/ShiftAssignments/AssignShift.cs, tests/ShiftFlow.UnitTests/Domain/ (HardRulesTests.cs, RuleEngineCatalogTests.cs, ShiftAssignmentAndRulesTests.cs, LeaveAndHr02Tests.cs), worklogs/POST-MVP-RULES-V2/Iteration-005.md
origen_cambios: "ver tabla Origen de cambios"
resultado: "PBI-016 implementado: contratos IHardRule, ISoftRule, RuleEvaluationContext, RuleEvaluationResult y RuleWarning; HR-01/02/03 como IHardRule con la semántica v1; stubs SR-01/SR-02 desactivados por defecto; RuleEngine orquesta el catálogo; AssignShift sin cambio observable. Build 0 avisos/0 errores; 54/54 tests unitarios (+28) y 35/35 de integración."
tiempo: PT0H12M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "Gate 0 (sdaf-gate0@0.2.0): G0.1 specs Approved (SPEC-DOM-008, SPEC-APP-006, SPEC-ACC-006, SPEC-PRD-004, v0.2.1, 2026-09-25); G0.2 criterios en SPEC-ACC-006 §2–3 y SPEC-DOM-008 §6; G0.3 ADR-009 Aceptado (2026-09-25T19:28, @mortiz-iadev); G0.4 PBI-016 enlaza DOM-008, APP-006, ACC-006 y ADR-009 con DoD; G0.5 este worklog. Veredicto: PASS. TDD off: sin configuración de proyecto ni elección humana; checks dotnet build y dotnet test. Alcance de aceptación de PBI-016: DoD del PBI (tests unitarios de dominio y regresión sin cambio observable); ACC-R2-01…05 y S01…S03 quedan para PBI-017 y PBI-023. Decisiones donde la spec deja margen: (1) EnabledOverrides opcional en RuleEvaluationContext; sin clave rige el default del catálogo (hard activas, soft apagadas) y una hard mandatory (HR-01) se evalúa siempre (SPEC-DOM-008 §3, §6.2); enganche para OrganizationRuleConfig de PBI-017, sin entidad ni persistencia. (2) Interfaces con la forma exacta de §2.1, sin miembros extra. (3) Orden de evaluación HR-01, HR-02, HR-03 igual que v1: HardViolations[0] y la respuesta HTTP no cambian. (4) Las soft se evalúan aunque haya hard; el caller decide (SPEC-APP-006 §5.5). (5) Stubs SR-01/SR-02 devuelven null (semántica en PBI-024/025). (6) Se elimina la firma v1 de Evaluate sin wrapper; los tests v1 solo cambian la llamada, no las aserciones. (7) El parámetro de HR-03 sigue saliendo de Organization.MinimumRestMinutes. Tamaño: ~720 líneas nuevas (~325 de dominio con XML docs obligatorios por CS1591 y ~396 de tests), por encima de la heurística de 400 por la DoD (tests por regla y de orquestación). Abierto para PBI-017: confirmar el override en el contexto frente a inyectar el catálogo activo en el motor, y validar códigos desconocidos en el comando de config. Gap menor: sin test unitario de aplicación de AssignShift (lo cubren CalendarAssignApiTests y RuleExplainApiTests); la doc de RuleViolation.cs sigue diciendo «Rule Engine v1». RDD (review-31c2f1d7457d3848, base-diff db7b63f..6eb0b72, riesgo medium, consentimiento granted por el humano, lente review-reliability, 52 s): aprobada y confirmada (target_already_acknowledged) con 2 hallazgos SUGGESTION no bloqueantes, trabajo aparte: R3-unknown-override-keys (RuleEngine.cs:90-93, claves de override sin validar contra el catálogo; validar en PBI-017) y R3-null-rule-element (RuleEngine.cs:35, elemento nulo en el catálogo explícito da NullReferenceException; guardas del constructor sin tests)."
pruebas_ejecutadas: "dotnet build ShiftFlow.sln (0 avisos, 0 errores); dotnet test tests/ShiftFlow.UnitTests (54/54); dotnet test tests/ShiftFlow.IntegrationTests (35/35, SQLite en memoria; CalendarAssignApiTests y RuleExplainApiTests 15/15); CR/LF de los .cs tocados; validate-worklog.py sobre este worklog; gentle-ai review status/start/capture-result/acknowledge-approved (review-31c2f1d7457d3848)"
estado: hecho
siguiente_agente: "testing-review (testing-review-pr post-merge del PR 22)"
commit: 6eb0b72
pr: 22
rama: feat/pbi-016-rule-catalog
sha: 6eb0b72a0d01c9a1ed1469db1fc84e07c380ed4f
resumen_acumulado: "domain-application-agent@0.3.0 + csharp-adr006-slice@0.3.0 — PBI-016 implementado tras Gate 0 PASS en el commit 6eb0b72; RDD review-31c2f1d7457d3848 (sobre 6eb0b72) y review-60a97d93c2f7901c (la que cuenta, rango db7b63f..767f5bf con el commit del worklog) aprobadas; rama feat/pbi-016-rule-catalog; PR 22 (mergeado)"
---

# PBI-016 / Iteration-005

## Línea de decisión

- Gate 0 PASS con G0.5 cubierto por este worklog (H05 §3; sdaf-gate0@0.2.0).
- Worklog en `POST-MVP-RULES-V2/` y no en una carpeta propia: continuidad del corte (Iteration-004 nombra el Gate 0 de PBI-016 como siguiente paso).
- Alcance de aceptación: la DoD de PBI-016; los escenarios ACC-R2 con configuración o `warnings` HTTP pertenecen a PBI-017 y PBI-023 (SPEC-ACC-006, línea 11).
- Contratos y catálogo según SPEC-DOM-008 §1, §2.1 y §3; semántica de HR-01/02/03 según SPEC-DOM-006; orquestación en dominio según ADR-009 y ADR-003.
- Sin cambio observable en AssignShift: se conserva el orden v1 y la primera violación hard sigue decidiendo la respuesta.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| `src/ShiftFlow.Domain/Rules/IHardRule.cs`, `ISoftRule.cs` | SPEC-DOM-008 §2.1 | Contratos |
| `src/ShiftFlow.Domain/Rules/RuleEvaluationContext.cs`, `RuleEvaluationResult.cs`, `RuleWarning.cs` | SPEC-DOM-008 §1 | `EnabledOverrides` opcional (§3) |
| `src/ShiftFlow.Domain/Rules/Hr01NoOverlapRule.cs`, `Hr02ActiveLeaveRule.cs`, `Hr03MinimumRestRule.cs`, `ShiftIntervals.cs` | SPEC-DOM-006; ADR-009 | Lógica v1 extraída sin cambios |
| `src/ShiftFlow.Domain/Rules/Sr01WeekendPreferenceRule.cs`, `Sr02ShiftTypePreferenceRule.cs`, `RuleCatalog.cs` | SPEC-DOM-008 §3, §5; PBI-016 DoD | Stubs soft desactivados por defecto |
| `src/ShiftFlow.Domain/Rules/RuleEngine.cs` | ADR-009; SPEC-DOM-008 §6 | Orquesta el catálogo |
| `src/ShiftFlow.Application/ShiftAssignments/AssignShift.cs` | PBI-016 DoD; SPEC-APP-006 §5.5 | Sin cambio observable |
| `tests/ShiftFlow.UnitTests/Domain/HardRulesTests.cs`, `RuleEngineCatalogTests.cs` | SPEC-DOM-008 §6; PBI-016 DoD | 28 tests nuevos |
| `tests/ShiftFlow.UnitTests/Domain/ShiftAssignmentAndRulesTests.cs`, `LeaveAndHr02Tests.cs` | SPEC-DOM-006 | Solo cambia la llamada; aserciones intactas |
