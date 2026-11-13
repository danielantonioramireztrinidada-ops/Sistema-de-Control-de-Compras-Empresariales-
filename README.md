# Sistema de Control de Compras Empresariales

## Descripción

Sistema web desarrollado con ASP.NET Core Web API para gestionar proveedores y compras empresariales. Permite registrar, consultar, actualizar y eliminar información, aplicando reglas de negocio para mantener la integridad de los datos.

## Tecnologías utilizadas

- **C#**
- **ASP.NET Core Web API**
- **.NET 10**
- **Entity Framework Core**
- **SQLite**
- **Swagger / OpenAPI**
- **Git y GitHub**

## Arquitectura del proyecto

El sistema está organizado mediante una arquitectura de tres capas:

### 1. SistemaCompras.Domain

Contiene las entidades principales y las clases base del dominio.

- Entidades: `Proveedor` y `Compra`.
- Clase base: `BaseEntity`.

### 2. SistemaCompras.Infrastructure

Contiene los componentes encargados del acceso y manejo de datos.

- Contexto de base de datos.
- Interfaces de repositorios.
- Repositorios genéricos y específicos.
- Modelos de transferencia de datos (DTO).
- Excepciones personalizadas.

### 3. SistemaComprasAPI

Contiene la capa de presentación y exposición de servicios REST.

- Controlador de proveedores.
- Controlador de compras.
- Configuración de servicios.
- Configuración de Entity Framework Core.
- Configuración de Swagger.

## Funcionalidades principales

### Gestión de proveedores

- Registrar proveedores.
- Consultar todos los proveedores.
- Consultar un proveedor por su identificador.
- Actualizar información de proveedores.
- Eliminar proveedores cuando no tengan compras asociadas.

### Gestión de compras

- Registrar compras.
- Consultar todas las compras.
- Consultar una compra por su identificador.
- Actualizar compras.
- Eliminar compras.
- Mostrar el nombre del proveedor asociado a cada compra.

## Reglas de negocio

El sistema implementa las siguientes validaciones:

1. El monto de una compra debe ser mayor que cero.
2. No se permite registrar una compra si el proveedor indicado no existe.
3. No se permite actualizar una compra asignándole un proveedor inexistente.
4. No se permite eliminar un proveedor que tenga compras registradas.
5. Se utiliza una relación entre proveedores y compras con restricción de eliminación para proteger la integridad referencial.

## Base de datos

Se utiliza SQLite como sistema de gestión de base de datos y Entity Framework Core para realizar las operaciones de persistencia.

Las entidades principales son:

- `Proveedores`
- `Compras`

La relación entre ambas entidades es de uno a muchos: un proveedor puede tener varias compras registradas.

## Requisitos previos

Para ejecutar el proyecto necesitas:

- .NET 10 SDK.
- Visual Studio Code o un entorno de desarrollo compatible.
- Git.

## Instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/danielantonioramireztrinidada-ops/Sistema-de-Control-de-Compras-Empresariales-.git
```

### 2. Entrar al proyecto

```bash
cd Sistema-de-Control-de-Compras-Empresariales-
```

### 3. Restaurar dependencias

```bash
dotnet restore SistemaComprasAPI/SistemaComprasAPI.csproj
```

### 4. Compilar el proyecto

```bash
dotnet build SistemaComprasAPI/SistemaComprasAPI.csproj
```

### 5. Ejecutar la API

```bash
dotnet run --project SistemaComprasAPI/SistemaComprasAPI.csproj --urls http://0.0.0.0:5001
```

## Documentación de la API

Una vez ejecutado el proyecto, Swagger permite visualizar y probar los endpoints disponibles.

En un entorno local:

```text
http://localhost:5001/swagger
```

En GitHub Codespaces, utiliza la URL del puerto 5001 y agrega `/swagger`.

## Endpoints principales

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `/api/Proveedores` | Listar proveedores |
| GET | `/api/Proveedores/{id}` | Consultar proveedor |
| POST | `/api/Proveedores` | Registrar proveedor |
| PUT | `/api/Proveedores/{id}` | Actualizar proveedor |
| DELETE | `/api/Proveedores/{id}` | Eliminar proveedor |
| GET | `/api/Compras` | Listar compras |
| GET | `/api/Compras/{id}` | Consultar compra |
| POST | `/api/Compras` | Registrar compra |
| PUT | `/api/Compras/{id}` | Actualizar compra |
| DELETE | `/api/Compras/{id}` | Eliminar compra |

## Autor

Proyecto académico desarrollado como práctica de desarrollo de aplicaciones web utilizando ASP.NET Core Web API y arquitectura por capas.