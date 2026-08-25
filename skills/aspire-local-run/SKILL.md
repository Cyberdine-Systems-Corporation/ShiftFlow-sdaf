---
name: aspire-local-run
description: Verifica o documenta el arranque del runtime local Aspire (o equivalente) del consumidor. Usar al validar Gate 2 local o al onboardear el entorno.
---

# aspire-local-run

| Campo | Valor |
|--------|--------|
| ID | aspire-local-run |
| Versión | 0.1.0 |
| Estado | Approved |
| Prioridad | media |
| Fecha | 2026-08-25 |
| Pack | sdaf-stack-dotnet@0.1.0 |
| Norma | Gate 2 (sdaf-core H05), runbook `docs/` del consumidor |

## Disparadores

- “¿Arranca en local?”, Gate 2.5, onboarding de runtime.
- Cambios que afecten AppHost / compose local.

## Pasos

1. Localizar el runbook del consumidor (`docs/` o README) — no inventar topología.
2. Arrancar según el ADR/runtime acordado (Aspire AppHost u otro documentado).
3. Comprobar health básico de los servicios In del MVP.
4. Si falla: listar gaps (config, secretos ausentes, puertos); no “arreglar” saltándose specs.
5. Registrar `aspire-local-run@0.1.0` y resultado en worklog.

## Definition of Done

- [ ] Comando/pasos de arranque citados
- [ ] Resultado OK o STOP con evidencias
- [ ] Worklog actualizado

## Restricciones

- No introducir secretos en el repo.
- No imponer Aspire si el ADR del consumidor eligió otro runtime: entonces documentar N/A y usar el runbook real.
- El nombre “aspire” es el playbook por defecto del pack; el consumidor manda.

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-08-25 | Primera versión del pack |
