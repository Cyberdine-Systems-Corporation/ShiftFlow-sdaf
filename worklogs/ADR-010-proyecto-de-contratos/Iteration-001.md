---
pbi: ADR-010-proyecto-de-contratos
iteracion: Iteration-001
fecha: 2026-11-01
inicio: 2026-11-01T10:52:00+01:00
fin: 2026-11-01T11:01:00+01:00
agente: humano + Claude Code (redacción de ADR propuesto)
modelo: claude-sonnet-5-5
version_prompt: "N/A"
prompt_base: "N/A: encargo directo del humano, sin prompt de agente SDAF"
prompts_adicionales: ninguno
skills: adr-propose
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: "Encargo del humano (2026-11-01): «comienza con ADR-010 teniendo en cuenta que acepto todas tus recomendaciones, y redáctalo para que pueda revisarlo»"
contexto: "Resolver la discrepancia Web → Application frente a ADR-004 (línea base 0.1.2, sección 7) con la opción B elegida por el humano el 2026-10-31: extraer un proyecto de contratos compartido por Api y Web. Esta iteración solo redacta el ADR como Propuesto; no toca código ni la línea base."
especificaciones_utilizadas: "architecture/decisions/ADR-004-layout-solucion.md, ADR-005, ADR-006, ADR-009 (formato), architecture/description.md 0.1.2, sdaf-core/templates/adr.md, sdaf-core/handbook/13 §2 y §9, sdaf-core/handbook/14 §6"
archivos_leidos: "architecture/decisions/*, architecture/description.md, src/*/*.csproj, src/ShiftFlow.Application/Auth/DemoCredentials.cs, src/Directory.Build.props, usos de AuthRoles en Infrastructure, inventario de solo lectura de tipos de Application usados por Web y Api"
archivos_modificados: "architecture/decisions/ADR-010-proyecto-de-contratos.md (nuevo; Propuesto y luego Aceptado), architecture/decisions/ADR-004-layout-solucion.md (anotado como enmendado por ADR-010), architecture/decisions/README.md (fila del índice), este worklog"
origen_cambios: "ADR y worklog: redacción del agente, pendiente de revisión humana"
resultado: "ADR-010 redactado con la unidad ShiftFlow.Contracts, el grafo de dependencias nuevo (enmienda ADR-004), DemoCredentials fuera del contrato, test de arquitectura, spec de integración y plan de línea base nueva. Aceptado por el humano (@mortiz-iadev, CODEOWNERS) el 2026-11-01T11:06+01:00 tras su revisión; el agente transcribió la aceptación y anotó ADR-004 como enmendado."
tiempo: PT8M
coste:
  modalidad: suscripcion
  importe: null
  moneda: null
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "El inicio (10:52) es una estimación. Sin cambios de código ni de la línea base: la versión con 8 unidades se escribe al terminar el refactor (H14 §6). El estado del ADR lo cambia solo un humano. Las fechas de las decisiones del humano (opción B el 2026-10-31; aceptación de las recomendaciones el 2026-11-01) no llevan hora registrada."
pruebas_ejecutadas: "check-adrs.py --consumer --legacy-up-to 7 (OK, 9 ADRs); comprobación de los enlaces del ADR; comprobación de las afirmaciones de hecho citadas (ProjectReference de Web, usos de AuthRoles en Infrastructure, records de entrada de Api, riesgo de la línea base)"
estado: hecho
siguiente_agente: humano (confirmar el commit; después abrir el PBI de ejecución con su spec de integración)
commit: null
pr: null
rama: docs/adr-010-contratos
sha: null
resumen_acumulado: "ad hoc — ADR-010 redactado y Aceptado por el humano (2026-11-01T11:06+01:00) en rama local; ADR-004 anotado; sin commit, PR ni cambios de código"
---

# ADR-010-proyecto-de-contratos / Iteration-001

## Línea de decisión

- Opción B (proyecto de contratos), elegida por el humano el 2026-10-31. El humano aceptó las recomendaciones de detalle el 2026-11-01 («acepto todas tus recomendaciones»): namespaces `ShiftFlow.Contracts.<Slice>` en commit aparte, `DemoCredentials` fuera del contrato y Web sin pre-rellenar el login, forma de ADR nuevo que enmienda ADR-004, test de arquitectura dentro del PBI de ejecución y spec de integración nueva.
- Corrección de mi plan anterior, ya aplicada en la línea base: la línea base 0.1.2 Approved describe la arquitectura vigente con la desviación registrada; la versión con 8 unidades se escribe y aprueba al terminar el refactor, no antes.
- Contenido de Contracts: los 9 DTOs que Web usa y que Api devuelve, más `AuthRoles`. Fuera: Commands, Queries, Handlers, `ToDto`, `DemoCredentials` y los records de entrada de Api.
- `Infrastructure → Contracts` se incluye en el grafo porque Infrastructure usa `AuthRoles` en `DependencyInjection.cs` e `IdentitySeed.cs`.
- Mientras el refactor no termine, el código sigue sin cumplir el grafo; el ADR lo dice y no lo exceptúa (una excepción exigiría un ADR con caducidad, H13 §3).
- Atribución: la elección y la aceptación de recomendaciones son del humano; la redacción del ADR y de este worklog es del agente. El agente no acepta el ADR.
- Aceptación (2026-11-01T11:06+01:00): instrucción humana explícita «Revisado el ADR-010, acéptalo». El agente transcribió `Estado: Aceptado`, la fila `Aceptación` y la versión 0.2.0 del historial, y anotó ADR-004 como «Enmendado por ADR-010» (punto 10 del ADR) sin reescribir su texto. La línea base 0.1.2 no se toca: añadir ADR-010 a su sección 6 y actualizar la fila de riesgo de la sección 7 es un cambio de un documento Approved y queda a decisión del humano.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| architecture/decisions/ADR-010-proyecto-de-contratos.md | IA, redacción revisada | Propuesto; la aceptación es humana |
| architecture/decisions/README.md | IA | Fila del índice con Estado Propuesto |
