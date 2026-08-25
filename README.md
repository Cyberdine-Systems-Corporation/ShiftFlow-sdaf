# ShiftFlow-sdaf

Reconstrucción de **ShiftFlow** como consumidor SDAF (paridad funcional con el producto original, gobernanza desacoplada).

| Componente | Pin |
|------------|-----|
| Método | `sdaf-core` @ **v0.2.0** |
| Pack | `sdaf-stack-dotnet` @ **v0.1.0** |
| Extract (fuente) | [`ShiftFlow-sdaf-extract`](https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf-extract) — solo importación, no base git |

## Estado actual

- Bootstrap SDAF + pack materializado (`agents/`, `skills/` de extensión).
- Handbook de producto en Draft (charter/MVP orientados a reconstrucción).
- **Sin** código/specs de producto aún → Gate 0 = STOP hasta importar y aprobar specs.
- Plan de importación: [docs/IMPORT-FROM-EXTRACT.md](docs/IMPORT-FROM-EXTRACT.md).

## Clonar

```powershell
git clone --recurse-submodules https://github.com/Cyberdine-Systems-Corporation/ShiftFlow-sdaf.git
cd ShiftFlow-sdaf
git submodule update --init --recursive
```

## Norma

- Método: `sdaf-core/handbook/` (no contradecir).
- Stack concreto: ADRs en `architecture/decisions/` + playbooks del pack.
- Gate 0 antes de implementar (`sdaf-core/skills/sdaf-gate0`).
