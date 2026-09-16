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

## 🧾 Worklogs

Los cambios de ingeniería relevantes registran un worklog (recibo de iteración + línea de decisión, [`handbook 08 §4.1–4.2`](sdaf-core/handbook/08-agent-traceability.md)). No reescribir los worklogs de [`worklogs/TRANSPLANTE/`](worklogs/TRANSPLANTE/) ni [`worklogs/INIT-REBUILD/`](worklogs/INIT-REBUILD/) — son histórico.

## 🔒 Secretos

> ⚠️ Nunca commitear contraseñas ni cadenas de conexión reales. Usa `dotnet user-secrets` o variables de entorno (ver [usuario demo](docs/runbook-local.md#8-usuario-demo)).
