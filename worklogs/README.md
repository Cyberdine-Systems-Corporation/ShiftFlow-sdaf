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
| [`UPGRADE-SDAF-0.4/`](UPGRADE-SDAF-0.4/Iteration-002.md) | Upgrade de gobernanza a sdaf-core v0.4.0 + sdaf-stack-dotnet v0.3.0; parche core v0.4.2 y adopción de gentle-ai (Iteration-002) |
| `FIX-ENLACES/` | Corrección de symlinks con destino corrupto y de enlaces de handbook |
| [`FIX-BOOTSTRAP/`](FIX-BOOTSTRAP/Iteration-001.md) | `BOOTSTRAP.md` alineado a core v0.4.2 y pack v0.3.0; codificación reparada |
| [`GENTLE-AI-RDD/`](GENTLE-AI-RDD/Iteration-001.md) | RDD de gentle-ai solo antes del commit (`--projection staged`), sin excepción H06 §7; `AGENTS.md` 0.4.3 y guion del piloto |
