---
name: blazor-bff-slice
description: Implementa un slice UI Blazor y/o BFF según specs Approved. Usar con agente frontend tras Gate 0.
---

# blazor-bff-slice

| Campo | Valor |
|--------|--------|
| ID | blazor-bff-slice |
| Versión | 0.1.0 |
| Estado | Approved |
| Prioridad | alta |
| Fecha | 2026-08-25 |
| Pack | sdaf-stack-dotnet@0.1.0 |
| Norma | playbooks/coding-standards-csharp.md, ADRs UI/API del consumidor, Gate 0 |

## Disparadores

- PBI de pantalla/flujo UI; “añadir BFF endpoint para la UI”.
- Agente `frontend` activo.

## Pasos

1. Confirmar Gate 0 y acceptance UI.
2. Respetar límites BFF/API del ADR del consumidor (no llamar dominio desde el browser si el ADR lo prohíbe).
3. Implementar componentes/páginas Blazor y endpoints BFF necesarios en `src_path`.
4. Reutilizar contratos ya expuestos por el slice de aplicación; no duplicar reglas de negocio en la UI.
5. Citar `blazor-bff-slice@0.1.0` en worklog; handoff a testing-review.

## Definition of Done

- [ ] Flujo UI del In observable según acceptance
- [ ] Sin lógica de dominio nueva en la UI
- [ ] Worklog cerrado

## Restricciones

- No Gate 0 skip; no secretos en cliente.
- Framework concreto lo fija el ADR del consumidor (este skill asume Blazor como playbook del pack).

## Referencias

- [agents/frontend-agent.md](../../agents/frontend-agent.md)

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-08-25 | Primera versión del pack |
