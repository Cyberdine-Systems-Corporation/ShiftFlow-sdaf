# AGENTS.md — Router de agentes ShiftFlow

| Campo | Valor |
|--------|--------|
| Versión | 0.2.0 |
| Estado | Draft |
| Fecha | 2026-08-25 |
| Norma | `sdaf-core/handbook/06`, `07`, `08`; pack `sdaf-stack-dotnet` |
| Config | `sdaf.config.yaml` |
| Core | `sdaf-core` @ v0.2.0 |
| Pack | `sdaf-stack-dotnet` @ v0.1.0 |

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

## Skills

- Core: [skills/](skills/) (`sdaf-gate0`, `sdaf-bootstrap`, `sdaf-upgrade`, …)
- Pack: `csharp-adr006-slice`, `blazor-bff-slice`, `aspire-local-run`
- Cursor: `.cursor/skills/<id>` → submodule (misma fuente que `skills/<id>`)

## Restricciones

No aprobar normas; no saltar Gate 0; no secretos; castellano en artefactos de ingeniería.
