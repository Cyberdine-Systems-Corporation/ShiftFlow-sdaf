---
pbi: UPGRADE-SDAF-0.5
iteracion: Iteration-001
fecha: 2026-10-31
inicio: 2026-10-31T11:33:00+01:00
fin: 2026-10-31T11:40:00+01:00
agente: humano + Claude Code (escritor acotado; adopción de la línea 0.5.0)
modelo: claude-sonnet-5-5
version_prompt: "N/A"
prompt_base: "N/A: encargo directo del humano, sin prompt de agente SDAF"
prompts_adicionales: ninguno
skills: sdaf-upgrade@0.3.1
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: encargo del humano (2026-10-31) con alcance local acotado; decisiones D3 y D4 del humano
contexto: Pin sdaf-core v0.5.3 (línea 0.5.0, opt-in) sobre un consumidor en v0.4.2 (línea 0.4.0). El submódulo ya estaba movido al tag en la rama chore/adopcion-sdaf-0.5.3. Esta iteración prepara en local los ficheros de la adopción; el agente no hace commit, push ni PR.
especificaciones_utilizadas: sdaf-core/docs/adopcion-y-upgrade.md (secciones 0.5.0 a 0.5.3, «Validar los ADRs del consumidor», «Comprobar Gate 0 en el PR»), sdaf-core/handbook/CHANGELOG.md, sdaf-core/handbook/14-architecture-description.md, sdaf-core/templates/architecture-description.md, sdaf-core/skills/sdaf-upgrade/SKILL.md, sdaf-core/AGENTS.md.template, sdaf-core/templates/worklog.md, sdaf-core/worklog.schema.json, sdaf-core/.github/actions/validate-sdaf/README.md
archivos_leidos: sdaf.config.yaml, AGENTS.md, README.md, BOOTSTRAP.md, .github/workflows/validate-sdaf.yml, architecture/decisions/README.md, architecture/decisions/ADR-001 a ADR-007 y ADR-009 (cabeceras y pasajes citados), worklogs/README.md, worklogs/UPGRADE-SDAF-0.4/Iteration-002.md, backlog/PBI-025-sr02-shift-type-preference.md, src/*/*.csproj y tests/*/*.csproj (solo ProjectReference), piloto-sdaf-0.5.0/description.borrador.md (fuera del repo, solo lectura)
archivos_modificados: sdaf-core (puntero a v0.5.3, ya movido antes de esta iteración), sdaf.config.yaml, AGENTS.md, README.md, BOOTSTRAP.md, docs/materializacion-submodules.md (pin vigente), .github/workflows/validate-sdaf.yml, architecture/description.md (nuevo), worklogs/UPGRADE-SDAF-0.5/Iteration-001.md (nuevo)
origen_cambios: N/A
resultado: Pins a v0.5.3 y sdaf.version "0.5.0" en config, AGENTS.md, README.md, BOOTSTRAP.md y CI; la action de CI en el SHA de v0.5.3 con check-adrs y adrs-legacy-up-to 7 (sin check-gate0); línea base architecture/description.md 0.1.0 en Draft. Sin commit, push ni PR. La línea base pendiente de revisión y aprobación humanas, por lo que G0.6 sigue en STOP.
tiempo: PT0H7M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "El inicio (11:33) es una estimación (hora de creación de la rama); el fin es la hora real. Comprobación informativa de Gate 0 (no se arregla; los PBIs antiguos no se tocan): check-gate0.py --pbi PBI-025 falla con «PBI desconocido» porque los PBIs del backlog no tienen la fila ID de la cabecera; con la ruta del PBI-025 da 13 FAIL (G0.4 por campos ID, Versión, Fecha, Acceptance, Unidades, ADRs y Worklogs ausentes; G0.1 por specs citadas que no existen como specs con ID; G0.2, G0.3 y G0.5 por campos ausentes; G0.6 porque la línea base está en Draft). Contraste de las reglas de dependencia con los ProjectReference: coinciden con ADR-004 salvo que ShiftFlow.Web referencia ShiftFlow.Application, relación que ADR-004 no lista. No se ha rematerializado la skill nueva sdaf-adopt-existing (v0.5.1) en skills/ ni .cursor/skills/. SECURITY.md y docs/materializacion-submodules.md aún citan pins antiguos (fuera del alcance acordado)."
pruebas_ejecutadas: validate-config.py sdaf.config.yaml (con y sin --strict-i4); check-adrs.py --consumer --legacy-up-to 7; validate-worklog.py sobre este worklog y sobre los 15 existentes con frontmatter; check-gate0.py --pbi PBI-025 y con la ruta del PBI; comprobación de que los enlaces simbólicos de agents/, prompts/agents/, skills/ y .cursor/skills/ resuelven; git status y git diff --stat
estado: en_curso
siguiente_agente: humano (revisión y aprobación de architecture/description.md; commit y PR bajo encargo)
commit: null
pr: null
rama: chore/adopcion-sdaf-0.5.3
sha: null
resumen_acumulado: "N/A — adopción de la línea 0.5.0 con pin sdaf-core v0.5.3 (sdaf-upgrade@0.3.1); línea base en Draft; rama chore/adopcion-sdaf-0.5.3; commit, PR y SHA ausentes"
---

# UPGRADE-SDAF-0.5 / Iteration-001

## Línea de decisión

- D3 (decisión del humano, 2026-10-31): la adopción incluye los pins a v0.5.3, `sdaf.version: "0.5.0"`, `architecture/description.md` como Draft, `check-adrs` en modo consumidor con `--legacy-up-to 7` en la CI y un worklog propio. No se activa `check-gate0` (adopcion-y-upgrade, «Comprobar Gate 0 en el PR»: un PR correctivo o de PBIs antiguos fallaría el paso).
- D4 (decisión del humano, 2026-10-31): la línea base queda en Draft. Un agente no la aprueba (H00 §3.3, H14 §5); la revisará y aprobará un humano después.
- `--legacy-up-to 7`: ADR-001 a ADR-007 son anteriores a la adopción y tienen formato antiguo (fecha sin hora, sin `Aceptación`); ADR-009 (número > 7) cumple las reglas del core. Lo fija el humano y queda revisable en el workflow (adopcion-y-upgrade, «Validar los ADRs del consumidor»). El hueco ADR-008 lo tolera el modo consumidor.
- `sdaf.version` pasa a `"0.5.0"` (adopcion-y-upgrade §0.5.0 paso 11 y Versionado). `stack.pack` y `tooling.*` no cambian: la guía no lo exige.
- Action de CI fijada al SHA completo de v0.5.3 (`75933a46a5ef604c2bb8f4d000ebb49de3928d00`), el mismo del submódulo. El checkout ya usa `fetch-depth: 0`, que `check-worklog` requiere; no se cambia.
- `AGENTS.md` a 0.5.0 (versionado propio del consumidor): se incorpora de la plantilla lo que exige la guía 0.5.0 paso 9 (norma H14 y precedencia única de H01 §3.2 en «Gobierno») más una línea que recuerda que G0.6 hace STOP mientras la línea base esté en Draft. No se incorpora la skill opcional `sdaf-adopt-existing` de la plantilla 0.5.1 (esta adopción no usa ese procedimiento) y el resto del documento no cambia.
- `BOOTSTRAP.md`: pins a v0.5.3, línea 0.5 y Gate 0 de G0.1 a G0.6 (H05 0.5.0 añade G0.6).
- `architecture/description.md`: adaptación del borrador del piloto (Draft, 7 unidades) al formato de H14 y de la plantilla de v0.5.3. Se conserva el contenido: unidades y reglas de ADR-004, `N/A: <motivo>` donde ningún ADR decide, sin unidades «heredada» y sin citar un ADR de adopción que no existe. Las rutas relativas del borrador (`decisions/…`) ya resuelven desde `architecture/`. Solo cambian la fecha y el historial.
- Sin tocar `worklogs/README.md` ni `architecture/decisions/README.md`: el formato de la línea 0.5.0 no los exige y los validadores pasan sin ellos. El índice de worklogs no lista aún la carpeta `UPGRADE-SDAF-0.5/`.
- Atribución: D3 y D4 son del humano; la redacción de los ficheros y de este worklog es del agente.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
