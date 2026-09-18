# PBI-017 — OrganizationRuleConfig + UI mínima

| Campo | Valor |
|--------|--------|
| Sprint | post-mvp-rules-v2 / plataforma |
| Prioridad | 17 |
| Specs | SPEC-DOM-008, SPEC-APP-006, SPEC-ACC-006 (Draft); ADR-009 Propuesto; ADR-007 (migraciones) |
| DoD | Persistencia config; API get/upsert; HR-01 no desactivable; UI mínima enable/params; ACC-R2-* |
| Estado | Pendiente |

## Descripción

Configuración por Organization del catálogo hard. Migración EF. Pantalla/sección Blazor admin mínima (sin redesign). Seed/default = paridad MVP. Dual-read de `MinimumRestMinutes` hacia params HR-03 si aplica.

## Dependencias

PBI-016.
