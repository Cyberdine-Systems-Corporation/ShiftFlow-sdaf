# Bootstrap â€” nuevo proyecto desde esta plantilla

Compatible con **sdaf-core@v0.2.0**. GuÃ­a larga del mÃ©todo: `sdaf-core/docs/adopcion-y-upgrade.md`. Skill: `sdaf-core/skills/sdaf-bootstrap`.

## 1. Renombrar el producto

1. Edita `sdaf.config.yaml` â†’ `project.name` (deja `stack.pack: null` salvo que adopts un pack).
2. Edita `AGENTS.md` (tÃ­tulo y fecha).
3. Edita `handbook/01-product-charter.md` y `handbook/03-mvp-definition.md` (Draft â†’ revisiÃ³n â†’ Approved).

## 2. Verificar el core

```powershell
git submodule status
# Debe mostrar sdaf-core (v0.2.0)
```

Si el submodule estÃ¡ vacÃ­o:

```powershell
git submodule update --init --recursive
cd sdaf-core
git checkout v0.2.0
cd ..
```

## 3. Rellenar el Ã¡rbol SDAF

| Paso | AcciÃ³n |
|------|--------|
| Knowledge | AÃ±ade fuentes en `knowledge/raw/` (append-only) |
| Specs | Draft â†’ revisiÃ³n humana â†’ **Approved** (`specs/`) |
| ADR | Si tocas stack/lÃ­mites â†’ `architecture/decisions/` |
| Backlog | PBI enlazado a specs |
| Worklog | `worklogs/<PBI>/Iteration-001.md` |

## 4. Gate 0

Antes de cÃ³digo en `src/`:

```text
Abrir sdaf-core/skills/sdaf-gate0/SKILL.md
Aplicar G0.1â€“G0.5 al PBI
```

En Cursor: *â€œAplica sdaf-core/skills/sdaf-gate0 al PBI-â€¦; no implementes si falla.â€*

En un repo reciÃ©n bootstrapado, Gate 0 â†’ **STOP** hasta specs Approved (esperado).

## 5. Pack de stack (opcional)

Esta plantilla **no** incluye pack. Para .NET:

1. AÃ±adir submodule `sdaf-stack-dotnet` @ `v0.2.0`.
2. `stack.pack: sdaf-stack-dotnet@0.1.0` en `sdaf.config.yaml`.
3. Seguir `sdaf-stack-dotnet/ADOPT.md` (materializar agentes/skills).

Referencia: [sdaf-smoke-core-pack](https://github.com/Cyberdine-Systems-Corporation/sdaf-smoke-core-pack).

## 6. ImplementaciÃ³n

Solo tras Gate 0. Los agentes de UI/implementaciÃ³n .NET los aporta el pack o contratos locales.

## 7. Upgrade del core

```powershell
cd sdaf-core
git fetch --tags
git checkout v0.2.x   # nueva release 0.2
cd ..
git add sdaf-core
git commit -m "chore: actualizar sdaf-core a v0.2.x."
```

O aplicar la skill `sdaf-core/skills/sdaf-upgrade`. Detalle: `sdaf-core/docs/adopcion-y-upgrade.md`.

