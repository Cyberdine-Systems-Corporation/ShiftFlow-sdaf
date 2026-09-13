# ShiftFlow-sdaf

Reconstrucción de **ShiftFlow** como consumidor SDAF (paridad funcional con el producto original, gobernanza desacoplada).

| Componente | Pin |
|------------|-----|
| Método | `sdaf-core` @ **v0.2.0** |
| Pack | `sdaf-stack-dotnet` @ **v0.1.0** |
| Extract (fuente) | [`ShiftFlow-sdaf-extract`](https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf-extract) — solo importación, no base git |

## Estado actual

- Gobernanza de producto: specs / handbook / ADRs **Approved** / **Aceptado**.
- Código MVP trasplantado desde el extract (`src/`, `tests/`) — build y tests verdes.
- Runbook: [`docs/runbook-local.md`](docs/runbook-local.md)
- Paridad: [`docs/PARITY-EXTRACT.md`](docs/PARITY-EXTRACT.md)
- Plan de importación: [`docs/IMPORT-FROM-EXTRACT.md`](docs/IMPORT-FROM-EXTRACT.md)

## Arranque local

```powershell
dotnet run --project src/ShiftFlow.AppHost --launch-profile https
```

Ver runbook para Compose, usuario demo y freeze.

## Clonar

```powershell
git clone --recurse-submodules https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf.git
cd ShiftFlow-sdaf
git submodule update --init --recursive
git config core.symlinks true
.\scripts\materialize-submodules.ps1 -Force
```

Ver [`docs/materializacion-submodules.md`](docs/materializacion-submodules.md) para prerequisitos (symlinks en Windows).

## Norma

- Método: `sdaf-core/handbook/` (no contradecir).
- Stack: ADRs en `architecture/decisions/` + playbooks del pack.
- Gate 0 antes de implementar features nuevas.
