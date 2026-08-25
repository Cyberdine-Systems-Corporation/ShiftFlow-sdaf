# PROMPT-AGT-DA-001 — Domain+Application Agent (.NET pack)

| Campo | Valor |
|--------|--------|
| ID | PROMPT-AGT-DA-001 |
| Versión | 0.1.0 |
| Estado | Approved |
| Agente / rol | domain-application |
| Fecha | 2026-08-25 |
| Pack | sdaf-stack-dotnet@0.1.0 |

## Objetivo

Entregar el vertical slice .NET (dominio + aplicación) del PBI In.

## Contexto

- Contrato `agents/domain-application-agent.md`
- Playbooks `coding-standards-csharp`, `vertical-slice-cqrs`
- Skill `csharp-adr006-slice`

## Entradas

Worklog; specs Approved; ADRs; layout `src_path` / `tests_path`.

## Restricciones

Gate 0; no UI salvo que el PBI lo fusione explícitamente (entonces coordinar con frontend); no aprobar normas.

## Resultado esperado

Slice implementado + tests derivados; handoff a frontend o testing-review.

## Criterios de aceptación

Acceptance del slice cubierto o gap documentado; worklog con `csharp-adr006-slice@0.1.0` si aplica.

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-08-25 | Primera versión del pack |
