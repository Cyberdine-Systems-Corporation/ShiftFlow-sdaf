# Architecture Decision Records

Decisiones de **stack y producto** del consumidor ShiftFlow-sdaf.

Plantilla: [`sdaf-core/templates/adr.md`](../../sdaf-core/templates/adr.md)  
Skill: [`sdaf-core/skills/adr-propose`](../../sdaf-core/skills/adr-propose/SKILL.md)

Importados desde `ShiftFlow-sdaf-extract`. **Aceptados** en batch 2026-08-27 (alineación `sdaf-core@0.2.0` + `sdaf-stack-dotnet@0.1.0`).

| ADR | Título | Estado |
|-----|--------|--------|
| [ADR-001](ADR-001-stack-tecnologico-mvp.md) | Stack tecnológico del MVP | Aceptado |
| [ADR-002](ADR-002-cliente-web-only-mvp.md) | Cliente Web-only (diferir MAUI) | Aceptado |
| [ADR-003](ADR-003-motores-planificacion-mvp.md) | Motores de planificación del MVP | Aceptado |
| [ADR-004](ADR-004-layout-solucion.md) | Layout de la solución .NET (skeleton) | Aceptado |
| [ADR-005](ADR-005-auth-basica-mvp.md) | Auth básica (Identity + cookie); código en PBI-002 | Aceptado |
| [ADR-006](ADR-006-coding-standards.md) | Estándares de código (regiones, XML docs, sin `var`) | Aceptado |
| [ADR-007](ADR-007-ef-migrations.md) | Evolución de esquema con migraciones EF Core | Aceptado |

**No importado:** ADR-008 (extracción SDAF al fork extract) — decisión histórica del laboratorio; el consumidor usa submodules pinneados.

**Pack:** `sdaf-stack-dotnet@0.1.0` materializa parte de ADR-006 vía `.cursor/rules/coding-standards-csharp.mdc` y playbooks del pack.
