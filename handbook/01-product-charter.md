# 01 — Product Charter

| Campo | Valor |
|--------|--------|
| Versión | 0.1.0 |
| Estado | Approved |
| Fecha | 2026-08-25 |

## Propósito

**ShiftFlow** reconstruido sobre SDAF: misma capacidad de producto que el ShiftFlow original, con método y pack desacoplados (`sdaf-core` + `sdaf-stack-dotnet`).

## Problema

Operar y evolucionar ShiftFlow sin acoplar la gobernanza SDAF al monolito histórico; recuperar paridad funcional en un repo consumidor limpio.

## Alcance In

- Capacidades del producto ShiftFlow (a detallar vía specs importadas del extract).
- Gobernanza SDAF 0.2 + pack .NET.

## Alcance Out

- Usar `ShiftFlow-sdaf-extract` como rama de producto.
- Inventar features fuera del MVP histórico sin enmienda.

## Éxito

Demo/acceptance equivalentes al ShiftFlow de referencia, con Gate 0–2 y worklogs auditables.

## Fuente

Importación planificada desde [ShiftFlow-sdaf-extract](https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf-extract) — ver `docs/IMPORT-FROM-EXTRACT.md`.
