# Importación desde ShiftFlow-sdaf-extract

Fuente: https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf-extract  

Este repo (**ShiftFlow-sdaf**) es el producto reconstruido. El extract **no** se mergea como historia base.

## Orden sugerido

1. **Knowledge** — copiar `knowledge/raw|curated` relevantes (sin reescritura silenciosa). ✅ *Hecho 2026-08-25* (`worklogs/INIT-REBUILD/Iteration-002.md`).
2. **Specs** — importar a `specs/`, remapear citas handbook 0.1→0.2, Draft hasta revisión → Approved. ✅ Import 2026-08-25; **Approved** batch 2026-08-27 (`Iteration-005`).
3. **Handbook producto** — caps. 02–04, 10–12, 16 (+ 01 charter). ✅ Import + **Approved** 2026-08-27.
4. **ADRs de stack** — ADR-001…007. ✅ Import; **Aceptado** batch 2026-08-27. ADR-008 excluido.
5. **Backlog** — 15 PBIs. ✅ Import; trasplante = Hecho 2026-08-27.
6. **Código** — trasplante `src/` / `tests/` desde extract (sin merge de historia; worklogs por sprint). ✅ *Hecho 2026-08-27* (`worklogs/TRANSPLANTE/Sprint-0…3.md`). Build+tests OK.
7. **Paridad** — checklist vs extract. ✅ Documentado en [`docs/PARITY-EXTRACT.md`](PARITY-EXTRACT.md) (arranque frío Aspire: pendiente humano).

## Prohibido

- Sustituir `sdaf-core` / pack por carpetas antiguas del extract.
- Implementar en `src/` sin Gate 0.
- Mezclar gobernanza 0.1 (capítulos 05–09 / 13–15) sin remap.
- Mergear historia git del extract como base del producto.
