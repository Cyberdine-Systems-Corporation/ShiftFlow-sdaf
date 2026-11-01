# ADR-010 — Proyecto de contratos compartidos entre Api y Web

| Campo | Valor |
|--------|--------|
| Estado | Aceptado |
| Fecha | 2026-11-01T11:06+01:00 |
| Decisores | Propone: agente Architecture. Acepta: identidad de [`CODEOWNERS`](../../CODEOWNERS) (sdaf-core H13 §2) |
| Aceptación | 2026-11-01T11:06+01:00 — @mortiz-iadev (CODEOWNERS), por instrucción humana explícita «Revisado el ADR-010, acéptalo» (sdaf-core H13 §2); transcripción del agente |
| Relacionado | ADR-004 (lo enmienda en el grafo de dependencias), ADR-005, ADR-006, ADR-001, [`description.md`](../description.md) §3 y §7, SPEC-PRD-001 (C-API, C-WEB) |

---

## Contexto

ADR-004 permite `Web → ServiceDefaults (+ HttpClient hacia Api)`, dice que Web consume la API por HTTP y que no referencia Infrastructure ni Domain, y exige enmendar el ADR para cambiar el grafo de dependencias.

La línea base arquitectónica (versión 0.1.2, sección 7) registra una discrepancia observada al contrastar las reglas con los `ProjectReference`:

- `ShiftFlow.Web.csproj` referencia `ShiftFlow.Application`, algo que ADR-004 no permite. Por transitividad, Web recibe también `Domain` y `MediatR`.
- De `Application`, Web usa solo 9 DTOs (`OrganizationDto`, `DepartmentDto`, `EmployeeDto`, `ShiftTypeDto`, `ShiftAssignmentDto`, `MonthCalendarDto`, `CalendarAssignmentDto`, `CalendarLeaveDto` y `LeaveDto`) y 2 clases de constantes (`AuthRoles` y `DemoCredentials`). No usa ningún Command ni Query de `MediatR`.
- Los endpoints de Api devuelven esos mismos DTOs directamente. Es decir, el contrato C-API/C-WEB existe, pero no tiene un lugar propio: vive dentro de `Application`, y en 7 ficheros comparte fichero con su Command y su Handler.

Ningún ADR decide hoy esta situación y la línea base Approved la mantiene registrada como riesgo, sin excepción ni enmienda. El humano eligió resolverla extrayendo un proyecto de contratos (opción B, 2026-10-31) y aceptó las recomendaciones de detalle que este ADR recoge (2026-11-01); la hora exacta de ambas decisiones no se registró.

---

## Decisión

1. **Se crea la unidad `ShiftFlow.Contracts`** (`src/ShiftFlow.Contracts`): el contrato de datos que comparten Api y Web (C-API y C-WEB).
2. **Contenido.** Los DTOs que Api devuelve y Web consume, y `AuthRoles`. **No** contiene Commands, Queries ni Handlers de `MediatR`, ni los mapeos `ToDto`, ni `DemoCredentials`, ni los records de entrada propios de Api (`NameBody`, `AssignShiftBody`, etc.), que se quedan donde están.
3. **Reglas del proyecto.**
   - Sin `ProjectReference` ni `PackageReference`: solo la BCL.
   - Records posicionales inmutables.
   - XML docs en castellano en todo miembro público (ADR-006; `CS1591` es error de compilación bajo `src/`).
   - Namespace `ShiftFlow.Contracts.<Slice>`, con los mismos slices que `Application` (Auth, Organizations, Departments, Employees, ShiftTypes, ShiftAssignments, Leaves). El renombrado de namespaces va en un commit aparte del movimiento de los tipos.
4. **Grafo de dependencias**, que sustituye al de ADR-004 («Dependencias permitidas»):

   ```text
   Web → Contracts, ServiceDefaults (+ HttpClient hacia Api)
   Api → Application, Infrastructure, Contracts, ServiceDefaults
   Infrastructure → Application, Domain, Contracts
   Application → Domain, Contracts
   Domain → (ninguna de ShiftFlow)
   Contracts → (ninguna)
   AppHost → Api, Web (proyectos ejecutables)
   ```

   Web no referencia `Application`, `Domain` ni `Infrastructure`, y con ello deja de recibir `Domain` y `MediatR` por transitividad. `Infrastructure → Contracts` se justifica porque usa `AuthRoles` (`DependencyInjection.cs` y `IdentitySeed.cs`). Los proyectos de pruebas no son unidades arquitectónicas, como en la línea base.
5. **`DemoCredentials` no pasa a Contracts.** Contiene la contraseña de desarrollo por defecto y no debe compilarse dentro de Web. Se queda en `Application`, y Web deja de pre-rellenar con ella el formulario de login. Cambio visible de la demo: el formulario aparece vacío y el usuario demo se documenta en el runbook (ADR-005).
6. **Verificación mecánica.** Un test de arquitectura en `tests/ShiftFlow.UnitTests` lee los `ProjectReference` y `PackageReference` de los `.csproj` y los compara con el grafo del punto 4. Falla si Web referencia `Application` o si `Contracts` referencia algo. Sustituye la verificación «Manual» de las reglas de la línea base que cubra.
7. **Spec del contrato.** La superficie compartida se describe en una spec de integración (`specs/integration/`) que un humano aprueba antes de ejecutar el refactor. Este ADR no la crea.
8. **Ejecución.** Es un PBI propio, no parte de este ADR. Se hace antes de los PBIs que añaden contratos (PBI-017 y PBI-023). Mientras ese PBI no termine, el código sigue sin cumplir ni el grafo de ADR-004 ni el nuevo: la desviación queda registrada en la sección 7 de la línea base y este ADR es su plan de retorno. Tolerarla formalmente con una fecha límite exigiría un ADR de excepción con caducidad (H13 §3); este ADR no lo es.
9. **Efecto en la línea base.** Al terminar el refactor, la línea base pasa a una versión nueva con 8 unidades y las reglas del punto 4, que aprueba un humano (H14 §6). Hasta entonces rige la versión 0.1.2 Approved.
10. **Efecto en ADR-004.** Cuando este ADR se acepte, ADR-004 se anota como «Enmendado por ADR-010» en su cabecera e historial, sin reescribir su texto.

---

## Alternativas consideradas

| Alternativa | Motivo de rechazo o aplazamiento |
|-------------|----------------------------------|
| Permitir `Web → Application` solo para contratos | Web seguiría recibiendo `Domain` y `MediatR` por transitividad, y «solo contratos» no sería verificable por herramienta |
| Excepción con caducidad (H13 §3) | Reconoce la deuda pero no la corrige; sin retorno automático a la regla. Aplazada: no se descarta si el refactor se retrasa |
| Duplicar los tipos dentro de Web | Duplica el contrato y puede divergir del que sirve Api |
| Mantener el namespace `ShiftFlow.Application.*` en el proyecto nuevo | Un ensamblado de contratos con nombre de `Application` confunde y oculta la dependencia real |
| Mover `DemoCredentials` a Contracts | Compilaría la contraseña por defecto de desarrollo dentro de Web |
| Unificar Api y Web en un solo host | Descartado en ADR-004 (mezcla composition root y UI) |

---

## Consecuencias

### Positivas

- Web depende solo del contrato y de ServiceDefaults; `Domain` y `MediatR` salen de su grafo.
- Las reglas de dependencia dejan de ser «Manual» y se verifican en cada build de tests.
- El contrato C-API/C-WEB tiene un lugar propio antes de PBI-017 y PBI-023, que añaden contratos.
- La línea base vuelve a coincidir con el código.

### Negativas / costes

- Una unidad más y un refactor: unos 9 ficheros nuevos, 11 tipos movidos y 7 ficheros partidos (cada DTO comparte fichero con su Command y su Handler).
- Con el renombrado de namespaces cambian los `using` de unos 11 ficheros de Web, 5 de Api, 3 de Infrastructure, unos 18 de Application y 9 tests de integración, además de la solución (`.sln`).
- Riesgo de serialización: Web deserializa con `PropertyNameCaseInsensitive` y Api usa los valores por defecto de ASP.NET. Se cubre con un test de la forma JSON de cada DTO.
- Cambio visible en la demo: el login deja de venir pre-rellenado.
- Documentación a actualizar: ADR-004, `docs/architecture.md`, `README.md`, `handbook/16-testing-framework.md` y las diapositivas del MVP.

### Cumplimiento

- Gate 0 del PBI de ejecución: este ADR Aceptado, la línea base Approved (G0.6), la spec de integración Approved y su acceptance.
- Tests: arquitectura (punto 6), forma JSON de los DTOs y la suite de integración existente.
- Línea base nueva (8 unidades) aprobada por un humano al terminar el PBI.

---

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.2.0 | 2026-11-01T11:06+01:00 | Aceptado por @mortiz-iadev (CODEOWNERS) tras su revisión; sin cambio de decisión respecto a 0.1.0. ADR-004 se anota como enmendado por este ADR (punto 10) |
| 0.1.0 | 2026-11-01T11:00+01:00 | Propuesto por el agente Architecture; pendiente de revisión y aceptación humanas |
