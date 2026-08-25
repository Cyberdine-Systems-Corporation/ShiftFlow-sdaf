# Importación desde ShiftFlow-sdaf-extract

Fuente: https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf-extract  

Este repo (**ShiftFlow-sdaf**) es el producto reconstruido. El extract **no** se mergea como historia base.

## Orden sugerido

1. **Knowledge** — copiar `knowledge/raw|curated` relevantes (sin reescritura silenciosa). ✅ *Hecho 2026-08-25* (`worklogs/INIT-REBUILD/Iteration-002.md`).
2. **Specs** — importar a `specs/`, remapear citas handbook 0.1→0.2 (`sdaf-core/docs/adopcion-y-upgrade.md`), dejar Draft hasta revisión humana → Approved. ✅ *Hecho 2026-08-25* — 20 specs en Draft.

**Handbook producto** — capítulos 02–04, 10–12, 16 importados (Draft); 01 conservado (charter reconstrucción). ✅ *Hecho 2026-08-25* (`Iteration-003`).
3. **ADRs de stack** — recrear/adaptar en `architecture/decisions/` (runtime, Blazor, BD); no copiar ciegamente ADRs del laboratorio si contradicen el pack/core. ✅ *Hecho 2026-08-25* — ADR-001…007 Propuesto; ADR-008 excluido (`worklogs/INIT-REBUILD/Iteration-003.md`).
4. **Backlog** — PBIs enlazados a specs importadas. ✅ *Hecho 2026-08-25* — 15 PBIs Pendiente (`worklogs/INIT-REBUILD/Iteration-004.md`).
5. **Código** — importar `src/` / `tests/` por vertical slices (skills del pack), no un dump único sin Gate 0.
6. **Paridad** — checklist “funciona como antes” contra demos/acceptance del extract.

## Prohibido

- Sustituir `sdaf-core` / pack por carpetas antiguas del extract.
- Implementar en `src/` sin Gate 0 por PBI.
- Mezclar gobernanza 0.1 (capítulos 05–09 / 13–15) sin remap.
