---
name: csharp-adr006-slice
description: Implementa un vertical slice .NET (API/aplicación + dominio) alineado a specs Approved. Usar tras Gate 0 en PBI de backend/aplicación.
---

# csharp-adr006-slice

| Campo | Valor |
|--------|--------|
| ID | csharp-adr006-slice |
| Versión | 0.1.0 |
| Estado | Approved |
| Prioridad | alta |
| Fecha | 2026-08-25 |
| Pack | sdaf-stack-dotnet@0.1.0 |
| Norma | playbooks/vertical-slice-cqrs.md, playbooks/coding-standards-csharp.md, Gate 0 (sdaf-core) |

## Disparadores

- “Implementar slice de API/aplicación”, PBI backend tras Architecture.
- Agente `domain-application` activo.

## Pasos

1. Confirmar Gate 0 y specs Approved del PBI.
2. Leer playbooks `vertical-slice-cqrs` y `coding-standards-csharp`.
3. Identificar comando/consulta, invariantes y acceptance del slice.
4. Implementar en el layout del consumidor (`src_path`): aplicación (+ dominio si la fusión lo exige) sin filtrar detalles de UI.
5. Añadir o ajustar tests en `tests_path` derivados de acceptance.
6. Registrar `csharp-adr006-slice@0.1.0` en worklog; handoff a frontend o testing-review.

## Definition of Done

- [ ] Slice In sin alcance Out
- [ ] Tests de acceptance del slice en verde o gap documentado
- [ ] Worklog actualizado

## Restricciones

- No saltar Gate 0; no aprobar specs/ADR.
- No meter nombres de producto ajenos ni secretos.
- El “ADR-006” del id es histórico de naming; el ADR vigente es el del **consumidor**.

## Referencias

- [playbooks/vertical-slice-cqrs.md](../../playbooks/vertical-slice-cqrs.md)
- [agents/domain-application-agent.md](../../agents/domain-application-agent.md)

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-08-25 | Primera versión del pack |
