# Worklog — TRANSPLANTE / Sprint-0

| Campo | Valor |
|--------|--------|
| Fecha | 2026-08-27 |
| Agente | architecture / devops |
| Skills | `csharp-adr006-slice@0.1.0`, `aspire-local-run@0.1.0` |
| Fuente | ShiftFlow-sdaf-extract (copia sin merge de historia) |
| Estado | hecho |
| PBIs | PBI-001, PBI-014 (árbol ya incluye migraciones EF) |

## Entregables

- `ShiftFlow.sln`, `.editorconfig`, `docker-compose.yml`
- `src/` (Domain, Application, Infrastructure, Api, Web, ServiceDefaults, AppHost)
- `tests/` (Unit + Integration)
- `docs/runbook-local.md`

## Verificación

| Comando | Resultado |
|---------|-----------|
| `dotnet build ShiftFlow.sln -c Release` | 0 errores, 0 warnings |
| `dotnet test` | Unit 26 OK; Integration 35 OK |

## Nota

El árbol del extract es indivisible a nivel de proyectos; Sprint 0 materializa el skeleton ADR-004 y, de hecho, trae el MVP completo. Los sprints 1–3 documentan el alcance funcional ya presente y cierran PBIs sin reescritura.
