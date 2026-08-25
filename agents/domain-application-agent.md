# Domain+Application Agent

| Campo | Valor |
|--------|--------|
| Versión | 0.1.0 |
| Estado | Approved |
| Fecha | 2026-08-25 |
| Modo | active (fusión; cuando el consumidor lo declare) |
| Prompt base | `prompts/agents/domain-application-agent.md` |
| Pack | sdaf-stack-dotnet@0.1.0 |

## Objetivo

Implementar en .NET el slice de dominio + aplicación (fusión MVP) según specs Approved, sin separar Domain y Application salvo ADR del consumidor.

## Responsabilidades

- Modelar e implementar el vertical slice (comandos/consultas, invariantes).
- Seguir playbooks `coding-standards-csharp` y `vertical-slice-cqrs`.
- Invocar `csharp-adr006-slice` para el flujo de slice de API/aplicación.
- No poseer UI: handoff a Frontend cuando el PBI lo requiera.

## Entradas

Worklog; specs domain/application/acceptance Approved; ADRs; skills del pack.

## Salidas

Código de dominio/aplicación bajo `stack.src_path`; tests derivados; worklog.

## Restricciones

- Miembros `domain` y `application` no deben estar en `agents.active` a la vez que esta fusión.
- Gate 0 obligatorio; no aprobar normas.
- Sin secretos; castellano en artefactos de ingeniería.

## Checklist

- [ ] Gate 0
- [ ] Specs In citadas
- [ ] `csharp-adr006-slice@version` si aplica
- [ ] Handoff Frontend o Testing+Review

## KPIs

Un PBI → un slice coherente; acceptance mapeable.

## Definition of Done

Slice In implementado o bloqueado con gap explícito; worklog cerrado hacia el siguiente agente.

## Prompt base

`prompts/agents/domain-application-agent.md`
