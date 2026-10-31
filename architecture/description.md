# Descripción de arquitectura — ShiftFlow

| Campo | Valor |
|--------|--------|
| Versión | 0.1.1 |
| Estado | Draft |
| Fecha | 2026-10-31T12:00+01:00 |
| Norma | `sdaf-core/handbook/14-architecture-description.md` |

> Línea base construida a partir de los ADRs vigentes del consumidor (H14 §9). No reescribe ningún ADR: enlaza cada afirmación a su decisión y escribe `N/A: <motivo>` en lo que ningún ADR establece. Solo un humano la pasa a Approved; mientras sea Draft, G0.6 hace STOP.

---

## 1. Contexto del sistema

| Elemento | Tipo | Relación con el sistema |
|----------|------|-------------------------|
| ShiftFlow | Sistema descrito | Planificación de turnos y calendario del MVP ([ADR-001](decisions/ADR-001-stack-tecnologico-mvp.md), [ADR-003](decisions/ADR-003-motores-planificacion-mvp.md)) |
| Usuario autenticado | Persona | Usa el cliente web; autenticación básica con Identity y cookie ([ADR-005](decisions/ADR-005-auth-basica-mvp.md)) |
| Base de datos relacional | Sistema de terceros | Persistencia del sistema, accedida solo desde Infrastructure ([ADR-001](decisions/ADR-001-stack-tecnologico-mvp.md)) |

N/A: los ADRs no declaran más actores ni sistemas externos; pendiente de redactar a partir de las specs de producto.

---

## 2. Unidades

| Unidad | Tipo | Responsabilidad |
|--------|------|-----------------|
| Domain | Proyecto | Modelo y puertos de dominio ([ADR-004](decisions/ADR-004-layout-solucion.md)) |
| Application | Proyecto | Casos de uso en cortes verticales, comandos y consultas ([ADR-004](decisions/ADR-004-layout-solucion.md)) |
| Infrastructure | Proyecto | Persistencia, base de datos y adaptadores ([ADR-004](decisions/ADR-004-layout-solucion.md), [ADR-007](decisions/ADR-007-ef-migrations.md)) |
| Api | Proyecto ejecutable | Host HTTP y raíz de composición de Application e Infrastructure ([ADR-004](decisions/ADR-004-layout-solucion.md)) |
| Web | Proyecto ejecutable | Única interfaz de cliente del MVP ([ADR-002](decisions/ADR-002-cliente-web-only-mvp.md), [ADR-004](decisions/ADR-004-layout-solucion.md)) |
| ServiceDefaults | Proyecto | Valores por defecto de observabilidad y salud ([ADR-004](decisions/ADR-004-layout-solucion.md)) |
| AppHost | Proyecto ejecutable | Orquestación local de la aplicación y sus dependencias ([ADR-004](decisions/ADR-004-layout-solucion.md)) |

N/A: los proyectos de pruebas no se declaran como unidades arquitectónicas.

---

## 3. Reglas de dependencia

| Desde | Hacia | Regla | Cómo se verifica |
|-------|-------|-------|------------------|
| Web | ServiceDefaults | Permitida | Manual |
| Web | Api | Permitida solo por HTTP; sin referencia de proyecto | Manual |
| Web | Domain, Infrastructure | Prohibida | Manual |
| Api | Application, Infrastructure, ServiceDefaults | Permitida | Manual |
| Infrastructure | Application, Domain | Permitida | Manual |
| Application | Domain | Permitida | Manual |
| Domain | Resto de unidades | Prohibida | Manual |
| AppHost | Api, Web | Permitida | Manual |

Fuente: [ADR-004](decisions/ADR-004-layout-solucion.md). N/A: ningún ADR declara una verificación automática; queda como manual hasta que se decida un test de arquitectura.

Discrepancia conocida con el código: `ShiftFlow.Web` referencia por proyecto a `ShiftFlow.Application`, algo que las reglas de arriba no permiten; véase el riesgo correspondiente en la sección 7.

---

## 4. Integraciones y contratos

| Origen | Destino | Contrato | Spec |
|--------|---------|----------|------|
| Web | Api | API HTTP del MVP ([ADR-004](decisions/ADR-004-layout-solucion.md)) | N/A: pendiente de enlazar la spec de integración |
| Application | Explicación de reglas | Puerto de explicación como stub, sin mutar el calendario ([ADR-009](decisions/ADR-009-rule-engine-v2-catalogo-hard.md)) | N/A: pendiente de enlazar la spec de integración |

N/A: no hay specs de integración (`specs/integration/`) todavía.

---

## 5. Atributos de calidad

N/A: ningún ADR prioriza atributos de calidad ni fija escenarios medibles; se redactará desde las specs de producto (H04 §5.5).

---

## 6. Decisiones vigentes

| ADR | Decisión | Unidades afectadas |
|-----|----------|--------------------|
| [ADR-001](decisions/ADR-001-stack-tecnologico-mvp.md) | Stack tecnológico del MVP | Todas |
| [ADR-002](decisions/ADR-002-cliente-web-only-mvp.md) | Cliente solo web en el MVP | Web |
| [ADR-003](decisions/ADR-003-motores-planificacion-mvp.md) | Motores de planificación del MVP; hard rules dentro del contexto de planificación | Domain, Application |
| [ADR-004](decisions/ADR-004-layout-solucion.md) | Layout de la solución, separación Api / Web y dependencias permitidas | Todas |
| [ADR-005](decisions/ADR-005-auth-basica-mvp.md) | Autenticación básica con Identity y cookie | Api, Web, Infrastructure |
| [ADR-006](decisions/ADR-006-coding-standards.md) | Estándares de código | Todas |
| [ADR-007](decisions/ADR-007-ef-migrations.md) | Evolución del esquema con migraciones | Infrastructure |
| [ADR-009](decisions/ADR-009-rule-engine-v2-catalogo-hard.md) | Catálogo de reglas duras y blandas configurable; extiende ADR-003 | Domain, Application, Infrastructure |

Todos están Aceptados y ninguno consta como sustituido. No existe ADR-008: la numeración de este consumidor tiene un hueco.

---

## 7. Riesgos y deuda

| Riesgo o deuda | Unidades afectadas | Mitigación o plan |
|----------------|--------------------|-------------------|
| El runtime elegido puede bloquear el avance de las herramientas | Todas | Contingencia declarada en [ADR-001](decisions/ADR-001-stack-tecnologico-mvp.md): enmienda hacia el runtime LTS anterior |
| Separación Api / Web revisable tras el MVP | Api, Web | [ADR-004](decisions/ADR-004-layout-solucion.md): revisable si la experiencia de desarrollo lo exige |
| Las reglas de dependencia solo se verifican a mano | Todas | Decidir un test de arquitectura en un ADR; hasta entonces, QG-Arch es manual |
| Hueco en la numeración de ADRs (no hay ADR-008) | Decisiones | Documentarlo o cubrirlo en el índice de ADRs |
| `ShiftFlow.Web` referencia por proyecto a `ShiftFlow.Application` (`ShiftFlow.Web.csproj`) y recibe `Domain` y `MediatR` por transitividad, lo que contradice las dependencias permitidas de [ADR-004](decisions/ADR-004-layout-solucion.md); Web usa de Application 9 DTOs y 2 clases de constantes | Web, Application | Pendiente de ADR: extraer un proyecto de contratos compartido por Api y Web mediante un ADR que enmiende ADR-004; hasta entonces la desviación no tiene excepción ni enmienda registradas |

---

## 8. Historial

| Versión | Fecha | Cambio |
|---------|--------|--------|
| 0.1.0 | 2026-10-31T11:38+01:00 | Borrador inicial construido desde los ADRs vigentes (H14 §9) en la adopción de la línea 0.5.0; redacción del agente, pendiente de revisión y aprobación humanas |
| 0.1.1 | 2026-10-31T12:00+01:00 | Se anota como riesgo la discrepancia conocida Web → Application (ADR-004) tras contrastar las reglas con los `ProjectReference`; corrección de redacción en un borrador sin aprobar, sin cambio de reglas; redacción del agente, pendiente de revisión y aprobación humanas |
