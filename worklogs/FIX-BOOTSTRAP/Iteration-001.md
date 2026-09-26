---
pbi: FIX-BOOTSTRAP
iteracion: Iteration-001
fecha: 2026-09-27
inicio: 2026-09-27T00:20:00+02:00
fin: 2026-09-27T00:35:00+02:00
agente: humano + Claude Code (corrección de documentación)
modelo: claude-opus-5-5
version_prompt: "N/A"
prompt_base: "N/A: encargo directo del humano, sin prompt de agente SDAF"
prompts_adicionales: ninguno
skills: ninguna
reglas_ide: idioma-castellano, git-remoto-encargo
ad_hoc: "plan aprobado por humano (alcance completo elegido por el humano: versiones y codificación)"
contexto: BOOTSTRAP.md tenía la cabecera en core v0.4.2 y pack v0.3.0, pero el cuerpo seguía en core v0.2.0 y pack v0.1.0, y 17 líneas con doble codificación (UTF-8 leído como cp1252).
especificaciones_utilizadas: sdaf-core/docs/adopcion-y-upgrade.md, sdaf-stack-dotnet/ADOPT.md, sdaf.config.yaml
archivos_leidos: BOOTSTRAP.md, sdaf.config.yaml
archivos_modificados: BOOTSTRAP.md, worklogs/README.md, worklogs/FIX-BOOTSTRAP/Iteration-001.md
origen_cambios: N/A
resultado: BOOTSTRAP.md alineado con core v0.4.2 (línea 0.4) y pack v0.3.0; 17 líneas con mojibake reparadas; texto y estructura sin cambios; CRLF conservado.
tiempo: PT0H15M
coste:
  modalidad: suscripcion
  importe: null
  moneda: EUR
  tokens: null
  fuente: "N/D: sesión de Claude Code por suscripción; sin contador de tokens por iteración"
observaciones: "Reparación por línea con un script de un solo uso: cada carácter vuelve a su byte cp1252 (latin-1 para bytes no definidos, p. ej. 0x9d) y se decodifica como UTF-8; las líneas ya correctas no se tocan."
pruebas_ejecutadas: búsqueda de marcas de mojibake (Ã, â€, â†) sin resultados; búsqueda de v0.1.x/v0.2.x sin resultados; file BOOTSTRAP.md (UTF-8, CRLF); validate-worklog.py sobre este worklog
estado: hecho
siguiente_agente: humano (revisión por CODEOWNERS)
commit: null
pr: null
rama: fix/bootstrap-versiones-codificacion
sha: null
resumen_acumulado: "Encargo directo — BOOTSTRAP.md alineado a core v0.4.2 y pack v0.3.0 y codificación reparada; rama fix/bootstrap-versiones-codificacion; commit, PR y SHA ausentes"
---

# FIX-BOOTSTRAP / Iteration-001

## Línea de decisión

- Pack `v0.3.0` y `stack.pack: sdaf-stack-dotnet@0.3.0`: pin vigente del consumidor (`sdaf.config.yaml`) y versión que documenta `sdaf-stack-dotnet/ADOPT.md`.
- Core `v0.4.2` en la verificación y el checkout inicial: pin recomendado (adopcion-y-upgrade, Versionado). En el upgrade, `v0.4.x`: último parche de la línea 0.4.
- Alcance ampliado a las versiones del core y a la codificación por decisión humana.

## Origen de cambios

| Archivo | Origen | Notas |
|---------|--------|-------|
| N/A (esta iteración no toca código de producto) | | |
