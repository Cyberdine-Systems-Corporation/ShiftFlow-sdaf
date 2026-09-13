# AGENTS.md — Router de agentes ShiftFlow

| Campo | Valor |
|--------|--------|
| Versión | 0.2.1 |
| Estado | Draft |
| Fecha | 2026-09-13 |
| Norma | `sdaf-core/handbook/06`, `07`, `08`; pack `sdaf-stack-dotnet` |
| Config | `sdaf.config.yaml` |
| Core | `sdaf-core` @ v0.2.1 |
| Pack | `sdaf-stack-dotnet` @ v0.1.1 |

---

## Propósito

Ingeniería de la reconstrucción de ShiftFlow. Gate 0 obligatorio antes de código en `src/`.

## Modelo

| Estado | Agentes |
|--------|---------|
| **Activo** | Specification, Architecture, Domain+Application, Frontend, Testing+Review |
| **Stub** | Product, Domain, Application, Infrastructure, DevOps, Review, Testing, AI |

## Handoff canónico

```text
Specification → Architecture → domain-application → frontend
                                      ↘ testing-review ↗
```

## Inventario

Contratos en `agents/` y prompts en `prompts/agents/` son **symlinks** al pin de core/pack; materializar con [`scripts/materialize-submodules.ps1`](scripts/materialize-submodules.ps1) (ver [`docs/materializacion-submodules.md`](docs/materializacion-submodules.md)).

### Núcleo (`sdaf-core`)

| Agente | Contrato | Estado |
|--------|----------|--------|
| Specification | [agents/specification-agent.md](agents/specification-agent.md) | active |
| Architecture | [agents/architecture-agent.md](agents/architecture-agent.md) | active |
| Testing+Review | [agents/testing-review-agent.md](agents/testing-review-agent.md) | active |
| Stubs Product/Domain/Application/DevOps/Review/Testing | [agents/](agents/) | stub |

### Pack (`sdaf-stack-dotnet`)

| Agente | Contrato | Estado |
|--------|----------|--------|
| Domain+Application | [agents/domain-application-agent.md](agents/domain-application-agent.md) | active |
| Frontend | [agents/frontend-agent.md](agents/frontend-agent.md) | active |
| Infrastructure | [agents/infrastructure-agent.md](agents/infrastructure-agent.md) | stub |

## Contexto autorizado (activos)

Índice. Abrir los artefactos; no concatenar en un mega-prompt. Si resumen y prompt discrepan, gana el prompt. Detalle en cada contrato.

| Agente | Rol | Flujo típico | IDE |
|--------|-----|--------------|-----|
| Specification | `prompts/agents/specification-agent.md` | `spec-draft-pbi`, `sdaf-worklog-handoff` | `idioma-castellano` |
| Architecture | `prompts/agents/architecture-agent.md` | `adr-propose`, `sdaf-worklog-handoff` | `idioma-castellano` |
| Testing+Review | `prompts/agents/testing-review-agent.md` | `sdaf-gate0`, `sdaf-worklog-handoff` | `idioma-castellano` |
| Domain+Application | `prompts/agents/domain-application-agent.md` | `csharp-adr006-slice`, `sdaf-gate0`, `sdaf-worklog-handoff` | `idioma-castellano`, `coding-standards-csharp` |
| Frontend | `prompts/agents/frontend-agent.md` | `blazor-bff-slice`, `sdaf-gate0`, `sdaf-worklog-handoff` | `idioma-castellano`, `coding-standards-csharp` |

**Stubs:** solo contrato + prompt base hasta activación humana explícita. `PROMPT-SYS-001` es gobernanza de director; no forma parte del paquete de implementación.

Worklogs **nuevos**: recibo de iteración + línea de decisión (H08 §4.1–4.2). No reescribir `worklogs/TRANSPLANTE/` ni `INIT-REBUILD/`.

## Skills

- Core: [skills/](skills/) (`sdaf-gate0`, `sdaf-bootstrap`, `sdaf-upgrade`, …)
- Pack: `csharp-adr006-slice`, `blazor-bff-slice`, `aspire-local-run`
- Cursor: `.cursor/skills/<id>` → submodule (misma fuente que `skills/<id>`)

## Restricciones

No aprobar normas; no saltar Gate 0; no secretos; castellano en artefactos de ingeniería.
