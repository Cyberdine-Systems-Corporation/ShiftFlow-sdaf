# Política de seguridad — ShiftFlow

| Campo | Valor |
|--------|--------|
| Versión | 0.1.0 |
| Estado | Draft |
| Fecha | 2026-09-25T11:33+02:00 |
| Norma | [sdaf-core/handbook/12-security-standards.md](sdaf-core/handbook/12-security-standards.md), plantilla [sdaf-core/templates/security.md](sdaf-core/templates/security.md) @ v0.4.0 |

No sustituye H12 ni el ADR de autenticación del producto.

---

## Cómo reportar

1. **No** abras un issue público con PoC explotable ni secretos.
2. Usa el reporte privado de vulnerabilidades de GitHub (pestaña **Security → Report a vulnerability** del repositorio). Lo recibe la identidad de [`CODEOWNERS`](CODEOWNERS).
3. Incluye: componente afectado, versión/commit, impacto, pasos **mínimos** (sin payload de ataque si se puede describir el defecto).

## Alcance

Baseline del método: [H12](sdaf-core/handbook/12-security-standards.md) (QG-Sec §5.2, secretos, authz según el ADR de auth del producto).

Fuera de alcance salvo ADR del producto: ASVS completo, pentest obligatorio, MFA/SSO, overlays de H12 §3 (SSDF, SBOM/AIBOM, AIMS / AI Act / CRA). Hasta que exista ese ADR, los overlays son **N/A**.

## Secretos

Nunca commitear contraseñas ni cadenas de conexión reales. Usar `dotnet user-secrets` o variables de entorno ([runbook local](docs/runbook-local.md)).

## Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-09-25T11:33+02:00 | Materializada desde `templates/security.md` de sdaf-core v0.4.0 (upgrade 0.2.1 → 0.4.0) |
