# Architecture Decision Records

Decisiones de **stack y producto** del consumidor ShiftFlow-sdaf.

Plantilla: [`sdaf-core/templates/adr.md`](../../sdaf-core/templates/adr.md)  
Skill: [`sdaf-core/skills/adr-propose`](../../sdaf-core/skills/adr-propose/SKILL.md)

Importados desde `ShiftFlow-sdaf-extract` (2026-08-25) en estado **Propuesto** — revisar alineación con `sdaf-core@0.2.0` y `sdaf-stack-dotnet@0.1.0` antes de aceptar.

| ADR | Título | Estado |
|-----|--------|--------|
| [ADR-001](ADR-001-stack-tecnologico-mvp.md) | Stack tecnológico del MVP | Propuesto |
| [ADR-002](ADR-002-cliente-web-only-mvp.md) | Cliente Web-only (diferir MAUI) | Propuesto |
| [ADR-003](ADR-003-motores-planificacion-mvp.md) | Motores de planificación del MVP | Propuesto |
| [ADR-004](ADR-004-layout-solucion.md) | Layout de la solución .NET (skeleton) | Propuesto |
| [ADR-005](ADR-005-auth-basica-mvp.md) | Auth básica (Identity + cookie); código en PBI-002 | Propuesto |
| [ADR-006](ADR-006-coding-standards.md) | Estándares de código (regiones, XML docs, sin `var`) | Propuesto |
| [ADR-007](ADR-007-ef-migrations.md) | Evolución de esquema con migraciones EF Core | Propuesto |

**No importado:** ADR-008 (extracción SDAF al fork extract) — decisión histórica del laboratorio; el consumidor usa submodules pinneados.

**Pack:** `sdaf-stack-dotnet@0.1.0` materializa parte de ADR-006 vía `.cursor/rules/coding-standards-csharp.mdc` y playbooks del pack.
