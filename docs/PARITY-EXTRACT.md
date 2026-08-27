# Paridad vs ShiftFlow-sdaf-extract

| Campo | Valor |
|--------|--------|
| Fecha | 2026-08-27 |
| Fuente | ShiftFlow-sdaf-extract |
| Destino | ShiftFlow-sdaf (`sdaf-core@v0.2.0` + `sdaf-stack-dotnet@0.1.0`) |

## Checklist

| # | Criterio | Estado |
|---|----------|--------|
| 1 | `dotnet build ShiftFlow.sln -c Release` limpio | OK (2026-08-27) |
| 2 | UnitTests verdes | OK (26) |
| 3 | IntegrationTests verdes | OK (35) |
| 4 | Journey SPEC-PRD-002 cubierto por tests API | OK (suite integración) |
| 5 | `GET /api/status` en tests | OK |
| 6 | Runbook presente (`docs/runbook-local.md`) | OK |
| 7 | Arranque en frío Aspire/Compose en esta máquina | Pendiente humano |
| 8 | Presentación C-PRE en `docs/presentation/` | OK (artefactos copiados; vídeo vía release histórico extract/ShiftFlow) |

## Diferencias explícitas vs extract

| Tema | Diferencia |
|------|------------|
| Gobernanza SDAF | Submodules pinneados 0.2 / pack 0.1; no carpeta `sdaf-core` del extract |
| Historia git | Sin merge; commits nuevos en este repo |
| Handbook método | Solo en submodule; producto en `handbook/` |
| ADR-008 | No importado (histórico del laboratorio) |

## Firma de paridad

Tras completar el ítem 7 (arranque local), se puede archivar el extract.
