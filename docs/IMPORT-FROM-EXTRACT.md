# Importación desde ShiftFlow-sdaf-extract

Fuente: https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf-extract  

Este repo (**ShiftFlow-sdaf**) es el producto reconstruido. El extract **no** se mergea como historia base.

## Orden sugerido

1. **Knowledge** — copiar `knowledge/raw|curated` relevantes (sin reescritura silenciosa).
2. **Specs** — importar a `specs/`, remapear citas handbook 0.1→0.2 (`sdaf-core/docs/adopcion-y-upgrade.md`), dejar Draft hasta revisión humana → Approved.
3. **ADRs de stack** — recrear/adaptar en `architecture/decisions/` (runtime, Blazor, BD); no copiar ciegamente ADRs del laboratorio si contradicen el pack/core.
4. **Backlog** — PBIs enlazados a specs importadas.
5. **Código** — importar `src/` / `tests/` por vertical slices (skills del pack), no un dump único sin Gate 0.
6. **Paridad** — checklist “funciona como antes” contra demos/acceptance del extract.

## Prohibido

- Sustituir `sdaf-core` / pack por carpetas antiguas del extract.
- Implementar en `src/` sin Gate 0 por PBI.
- Mezclar gobernanza 0.1 (capítulos 05–09 / 13–15) sin remap.
