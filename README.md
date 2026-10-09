# Sistema de Control de Compras Empresariales

API REST desarrollada con ASP.NET Core para administrar proveedores y compras empresariales.

## Tecnologías
- C# y ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

## Entidades
- **Proveedor:** almacena nombre, teléfono y correo.
- **Compra:** almacena descripción, monto, fecha y el proveedor relacionado.

Cada compra pertenece a un proveedor mediante `ProveedorId`.

## Operaciones CRUD

### Proveedores
- GET `/api/Proveedores`
- GET `/api/Proveedores/{id}`
- POST `/api/Proveedores`
- PUT `/api/Proveedores/{id}`
- DELETE `/api/Proveedores/{id}`

### Compras
- GET `/api/Compras`
- GET `/api/Compras/{id}`
- POST `/api/Compras`
- PUT `/api/Compras/{id}`
- DELETE `/api/Compras/{id}`

## Ejecutar el proyecto

1. Instalar el SDK de .NET compatible con el proyecto.
2. Restaurar las dependencias:

   `dotnet restore`

3. Compilar:

   `dotnet build`

4. Ejecutar:

   `dotnet run --urls http://localhost:5000`

5. Abrir Swagger en `/swagger`.

La base de datos SQLite se crea automáticamente al iniciar la aplicación.
