# 🔌 Referencia de API

| Campo | Valor |
|-------|-------|
| Relacionado | [ADR-005](../architecture/decisions/ADR-005-auth-basica-mvp.md), [ADR-007](../architecture/decisions/ADR-007-ef-migrations.md), [PBI-002](../backlog/PBI-002-auth-roles.md), [PBI-003](../backlog/PBI-003-organization-department-employee.md), [PBI-004](../backlog/PBI-004-shift-types.md), [PBI-005](../backlog/PBI-005-calendar-assign-shift.md), [PBI-007](../backlog/PBI-007-leaves.md), [PBI-011](../backlog/PBI-011-ai-explain-stub.md) |

Todos los endpoints son anónimos **salvo** `POST /api/auth/login`, `GET /api/status` y los health checks de Aspire (`/health`, `/alive`, solo Development); el resto requiere autenticación ([ADR-005](../architecture/decisions/ADR-005-auth-basica-mvp.md)), y los de maestros/calendario/ausencias/reglas exigen además el rol `Administrator`. Esquema OpenAPI autogenerado disponible en `/openapi/v1.json` **solo en Development** (no hay Swagger UI montada actualmente — ver [limitaciones conocidas](architecture.md#limitaciones-conocidas)).

Formato de error habitual: `400 { "error": "...", "code": "..." }`. Las violaciones de regla (`AssignShift`) añaden `title`, `body` y `mutatesSchedule` — ver [Rule Engine](architecture.md).

---

## 🔑 Auth

| Método | Ruta | Body | Respuesta |
|--------|------|------|-----------|
| `POST` | `/api/auth/login` | `{ "userName": "demo.admin", "password": "..." }` | `200 { userName, roles[], accessToken, sessionCookies[] }` / `401` |
| `POST` | `/api/auth/logout` | — | `200 { status: "logged_out" }` |
| `GET` | `/api/auth/me` | — | `200 { userName, roles[] }` / `401` |

## 🏢 Organizaciones

| Método | Ruta | Body | Notas |
|--------|------|------|-------|
| `POST` | `/api/organizations` | `{ name }` | `201` |
| `GET` | `/api/organizations` | — | Lista |
| `GET` | `/api/organizations/{id}` | — | `404` si no existe |
| `PUT` | `/api/organizations/{id}/name` | `{ name }` | |
| `PUT` | `/api/organizations/{id}/active` | `{ isActive }` | |
| `PUT` | `/api/organizations/{id}/minimum-rest` | `{ minimumRestMinutes }` | Umbral de `HR-03` (ver [Rule Engine](architecture.md)) |

## 🗂️ Departamentos

| Método | Ruta | Body |
|--------|------|------|
| `POST` | `/api/organizations/{organizationId}/departments` | `{ name }` |
| `GET` | `/api/organizations/{organizationId}/departments` | — |
| `PUT` | `/api/departments/{id}/name` | `{ name }` |
| `PUT` | `/api/departments/{id}/active` | `{ isActive }` |
| `GET` | `/api/departments/{departmentId}/employees` | — |

## 👥 Empleados

| Método | Ruta | Body |
|--------|------|------|
| `POST` | `/api/organizations/{organizationId}/employees` | `{ departmentId, displayName, email? }` |
| `GET` | `/api/organizations/{organizationId}/employees` | — |
| `PUT` | `/api/employees/{id}` | `{ departmentId, displayName, email? }` |
| `PUT` | `/api/employees/{id}/active` | `{ isActive }` |

## 🕒 Tipos de turno

| Método | Ruta | Body |
|--------|------|------|
| `POST` | `/api/organizations/{organizationId}/shift-types` | `{ name, code?, defaultStartTime?, defaultEndTime? }` |
| `GET` | `/api/organizations/{organizationId}/shift-types` | — |
| `PUT` | `/api/shift-types/{id}` | igual que alta |
| `PUT` | `/api/shift-types/{id}/active` | `{ isActive }` |

## 📅 Calendario y asignaciones

| Método | Ruta | Body / Query | Respuesta |
|--------|------|---------------|-----------|
| `GET` | `/api/organizations/{organizationId}/calendar?year=&month=` | query | `200` DTO del mes |
| `POST` | `/api/organizations/{organizationId}/assignments` | `{ employeeId, shiftTypeId, startAt, endAt }` (UTC) | `201 Location: /api/assignments/{id}` / `400` (`HR-01`/`HR-02`/`HR-03` con `title`+`body`, o `INV-CAL-01` de rango) |
| `POST` | `/api/assignments/{id}/cancel` | — | `200` / `404` |

Recorrido funcional paso a paso: [`docs/user-guide.md`](user-guide.md).

## 🌴 Ausencias

| Método | Ruta | Body / Query | Respuesta |
|--------|------|---------------|-----------|
| `GET` | `/api/organizations/{organizationId}/leaves?employeeId=&year=&month=&activeOnly=` | query (`activeOnly` default `true`) | `200` lista |
| `POST` | `/api/organizations/{organizationId}/leaves` | `{ employeeId, startOn, endOn, kind?, reason? }` (fechas inclusive) | `201` / `400 INV-LEA-RANGE` |
| `POST` | `/api/leaves/{id}/cancel` | — | `200` / `404` |

## Reglas

| Método | Ruta | Query | Respuesta |
|--------|------|-------|-----------|
| `GET` | `/api/rules/explain` | `code` (`HR-01`\|`HR-02`\|`HR-03`), `organizationId?`, `employeeId?`, `startAt?`, `endAt?` | `200` explicación / `400 INV-RUL-01` si `code` no soportado |

## 📶 Estado

| Método | Ruta | Respuesta |
|--------|------|-----------|
| `GET` | `/api/status` | `200 { service: "ShiftFlow.Api", status: "ok", database: "reachable"\|"unreachable" }` (anónimo) |
| `GET` | `/health` | Health check de Aspire (solo Development) |
| `GET` | `/alive` | Liveness check de Aspire (solo Development) |
