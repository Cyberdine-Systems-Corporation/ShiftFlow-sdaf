# 🤝 Contribuir a ShiftFlow

## 🚦 Antes de escribir código

1. Debe existir una **spec Approved** que cubra el cambio ([`specs/`](specs/README.md)) — es Gate 0. Sin spec aprobada no se implementa en `src/`.
2. Si el cambio implica una decisión de stack o arquitectura, primero un ADR en [`architecture/decisions/`](architecture/decisions/README.md) (plantilla en [`sdaf-core/templates/adr.md`](sdaf-core/templates/adr.md)).

## 🧑‍💻 Estándares de código (.NET)

Definidos en [`ADR-006`](architecture/decisions/ADR-006-coding-standards.md) y materializados en [`.cursor/rules/coding-standards-csharp.mdc`](.cursor/rules/coding-standards-csharp.mdc) (pack [`sdaf-stack-dotnet`](sdaf-stack-dotnet/README.md)):

- Regiones (`#region`) para separar endpoints/contratos en archivos grandes.
- Documentación XML (`///`) en tipos y miembros públicos.
- Sin `var`: tipos explícitos.
- CQRS por vertical slice vía MediatR (ver [`handbook/12-cqrs-vertical-slices.md`](handbook/12-cqrs-vertical-slices.md)) — un Command/Query + Handler por caso de uso, no controladores gordos.

## 🧪 Compilar y testear

```powershell
dotnet restore ShiftFlow.sln
dotnet build ShiftFlow.sln
dotnet test ShiftFlow.sln
```

Solo unitarios o solo integración (más rápido al iterar):

```powershell
dotnet test tests/ShiftFlow.UnitTests
dotnet test tests/ShiftFlow.IntegrationTests
```

Los tests de integración usan SQLite + `EnsureCreated`; no requieren PostgreSQL. Antes de abrir PR, build y tests deben quedar en verde.

## 🗃️ Migraciones EF Core

Si el cambio toca el modelo de datos, genera migración y commitéala (ver [`ADR-007`](architecture/decisions/ADR-007-ef-migrations.md) y [`docs/runbook-local.md` §3.1](docs/runbook-local.md#31-migraciones-ef-core-adr-007--pbi-014)):

```powershell
dotnet ef migrations add <Nombre> --project src/ShiftFlow.Infrastructure --startup-project src/ShiftFlow.Api --output-dir Persistence/Migrations --context ShiftFlowDbContext
```

## 📝 Commits y Pull Requests

- Mensajes de commit, títulos y cuerpos de PR/issues: **en castellano**, prosa breve centrada en el porqué. Prefijo opcional (`feat:`, `fix:`, `docs:`, `chore:`).
- Evitar mensajes solo en inglés tipo "Add X" / "Update Y".
- El cuerpo del PR declara la **clase del cambio** ([H13 §6](sdaf-core/handbook/13-enmienda-excepciones-ciclo-de-vida.md#6-regla-de-bump-y-commits)): *significado* (cambia una obligación de un artefacto Approved → bump minor + historial) o *redacción* (sin cambio de norma → bump patch).
- Revisión y merge: la identidad de [`CODEOWNERS`](CODEOWNERS) (QG-Review, H10 / H13 §7). Sin auto-merge ni force-push.
- Agentes de IA: commit local y cualquier escritura al remoto (push, PR, merge) **solo** si el mensaje humano de ese turno lo pide ([H06 §7](sdaf-core/handbook/06-ai-agent-framework.md#7-restricciones-globales), regla `.cursor/rules/git-remoto-encargo.mdc`).
- Fechas nuevas en cabeceras, historiales, ADRs y worklogs: ISO 8601 con hora y zona (`AAAA-MM-DDThh:mm±hh:mm`, H13 §9). Las fechas ya publicadas no se completan.

## 🧾 Worklogs

Los worklogs **nuevos** usan el frontmatter de [`sdaf-core/templates/worklog.md`](sdaf-core/templates/worklog.md) (`commit`, `pr`, `rama`, `sha` o `null`), con línea de decisión y origen de cambios ([`handbook 08 §4.1–4.3`](sdaf-core/handbook/08-agent-traceability.md)). Los worklogs en tabla siguen válidos; no se reescriben. No reescribir los worklogs de [`worklogs/TRANSPLANTE/`](worklogs/TRANSPLANTE/) ni [`worklogs/INIT-REBUILD/`](worklogs/INIT-REBUILD/) — son histórico.

## 🔐 Seguridad

Reporte de vulnerabilidades y alcance de QG-Sec: [`SECURITY.md`](SECURITY.md).

## 🔒 Secretos

> ⚠️ Nunca commitear contraseñas ni cadenas de conexión reales. Usa `dotnet user-secrets` o variables de entorno (ver [usuario demo](docs/runbook-local.md#8-usuario-demo)).
