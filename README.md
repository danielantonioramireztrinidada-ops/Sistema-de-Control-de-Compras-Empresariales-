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

El sistema utiliza una arquitectura por capas, organizada en los siguientes proyectos:

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

### 3. SistemaCompras.Application

Contiene la lógica de negocio y la capa de servicios.

- `Contract`: interfaces de los servicios de negocio.
- `Dtos`: objetos de transferencia de datos para crear, actualizar y consultar proveedores y compras.
- `Services`: implementación de los servicios de proveedores y compras.
- Validaciones de los campos de las entidades.
- Reglas de negocio para proteger la integridad de los datos.

### 4. SistemaComprasAPI

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

## Validaciones y reglas de negocio

- El nombre del proveedor es obligatorio y debe tener entre 2 y 100 caracteres.
- El teléfono del proveedor es obligatorio y debe tener un formato válido.
- El correo del proveedor es obligatorio y debe tener un formato válido.
- La descripción de una compra debe tener entre 3 y 200 caracteres.
- El monto de una compra debe ser mayor que cero.
- La fecha de la compra es obligatoria.
- El proveedor asociado a una compra debe existir.
- No se permite eliminar un proveedor que tenga compras registradas.

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