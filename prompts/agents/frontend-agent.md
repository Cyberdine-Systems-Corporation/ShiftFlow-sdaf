# PROMPT-AGT-FE-001 — Frontend Agent (.NET pack)

| Campo | Valor |
|--------|--------|
| ID | PROMPT-AGT-FE-001 |
| Versión | 0.1.0 |
| Estado | Approved |
| Agente / rol | frontend |
| Fecha | 2026-08-25 |
| Pack | sdaf-stack-dotnet@0.1.0 |

## Objetivo

Implementar UI/BFF del PBI según specs Approved y ADRs, sin ampliar Out.

## Contexto

- Contrato `agents/frontend-agent.md`
- Handbook método (sdaf-core); handbook de producto del consumidor
- Skills: `blazor-bff-slice`; playbook coding standards C#

## Entradas

PBI; worklog; rutas de specs/ADR; `sdaf.config.yaml` (`src_path`).

## Restricciones

Gate 0; no aprobar normas; no secretos; castellano; citar `skill@version` y prompt en worklog.

## Resultado esperado

Diff UI/BFF acotado; tests si la acceptance lo exige; handoff Testing+Review.

## Criterios de aceptación

Trazable a acceptance; límites API respetados; worklog cerrado.

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-08-25 | Primera versión del pack |
