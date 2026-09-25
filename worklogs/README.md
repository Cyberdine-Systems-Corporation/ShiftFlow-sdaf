# Worklogs (ATF)

Una carpeta por PBI; `Iteration-NNN.md` por ciclo.  
Plantilla: `sdaf-core/templates/worklog.md`  
Skill: `sdaf-core/skills/sdaf-worklog-handoff`

Desde la línea 0.4.0 del core, los worklogs **nuevos** llevan frontmatter YAML (`commit`, `pr`, `rama`, `sha` o `null`) y los valida `sdaf-core/scripts/validate-worklog.py` en CI. Los worklogs en tabla anteriores siguen válidos; no se reescriben.

| Carpeta | Tema |
|---------|------|
| `TRANSPLANTE/` | Histórico trasplante MVP (no reescribir) |
| `INIT-REBUILD/` | Histórico init (no reescribir) |
| `POST-MVP-RULES-V2/` | Corte Rule Engine hard + soft configurable (Iteration-003 con frontmatter 0.4.0) |
| [`UPGRADE-SDAF-0.4/`](UPGRADE-SDAF-0.4/Iteration-001.md) | Upgrade de gobernanza a sdaf-core v0.4.0 + sdaf-stack-dotnet v0.3.0 |
