# Sistema de Envíos v2

Sistema completo de gestión de envíos desarrollado con arquitectura en capas, que incluye una API RESTful en .NET 8 y una aplicación web MVC para la interfaz de usuario.

## 📋 Descripción

Sistema de gestión de envíos que permite a los usuarios realizar seguimiento de paquetes, gestionar envíos, agregar comentarios y administrar usuarios. El sistema implementa diferentes tipos de envíos (comunes y urgentes) con autenticación JWT.

## 🏗️ Arquitectura

El proyecto está dividido en dos aplicaciones principales:

### WebAPI
API RESTful desarrollada con .NET 8 siguiendo arquitectura en capas:

- **WebAPI**: Capa de presentación con controladores y autenticación JWT
- **LogicaAplicacion**: Casos de uso e interfaces de aplicación
- **LogicaNegocio**: Entidades de dominio, Value Objects y reglas de negocio
- **LogicaAccesoDatos**: Repositorios y acceso a base de datos con Entity Framework Core
- **Compartido**: DTOs, Mappers y excepciones compartidas

### MVC
Aplicación web ASP.NET Core MVC que consume la WebAPI:

- **Controllers**: Controladores MVC (Usuario, EnvioComun, Home)
- **Models**: ViewModels para vistas
- **Views**: Interfaces de usuario con Razor Pages
- **wwwroot**: Recursos estáticos (CSS, JS, librerías)

## 🚀 Tecnologías

- **.NET 8**
- **ASP.NET Core MVC**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server / LocalDB**
- **JWT Authentication**
- **Swagger/OpenAPI**
- **Bootstrap**
- **jQuery**

## 📦 Características Principales

### Gestión de Envíos
- ✅ Búsqueda de envíos por número de tracking
- ✅ Listado de envíos por cliente
- ✅ Filtrado de envíos por fecha
- ✅ Filtrado de envíos por comentarios
- ✅ Gestión de estados de envíos (En proceso, Finalizado)
- ✅ Soporte para envíos comunes y urgentes

### Gestión de Usuarios
- ✅ Sistema de autenticación con JWT
- ✅ Login de usuarios
- ✅ Cambio de contraseña
- ✅ Roles de usuario (Cliente, Empleado)
- ✅ Manejo de sesiones

### Comentarios y Auditoría
- ✅ Sistema de comentarios en envíos
- ✅ Auditoría de acciones
- ✅ Trazabilidad de cambios

## 🛠️ Instalación y Configuración

### Prerrequisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server o SQL Server LocalDB
- Visual Studio 2022 o VS Code

### Configuración de Base de Datos

1. **Configurar la cadena de conexión en `appsettings.json`:**

**WebAPI** (`WebAPI/WebAPI/appsettings.json`):
```json
{
  "ConnectionStrings": {
    "cadenaConexion": "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True; Initial Catalog=Obligatorio1P3DB"
  }
}
```

**MVC** (`MVC/appsettings.json`):
```json
{
  "ConnectionStrings": {
    "cadenaConexion": "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True; Initial Catalog=Obligatorio1P3DB"
  },
  "url": "https://localhost:7160/api/"
}
```

2. **Aplicar migraciones de Entity Framework:**

```powershell
cd WebAPI\LogicaAccesoDatos
dotnet ef database update --startup-project ..\WebAPI\WebAPI.csproj
```

### Ejecutar la Aplicación

1. **Iniciar la WebAPI:**

```powershell
cd WebAPI\WebAPI
dotnet run
```

La API estará disponible en: `https://localhost:7160`
Swagger UI: `https://localhost:7160/swagger`

2. **Iniciar la aplicación MVC (en otra terminal):**

```powershell
cd MVC
dotnet run
```

La aplicación web estará disponible en: `https://localhost:7xxx` (el puerto se mostrará en la terminal)

## 📂 Estructura del Proyecto

```
Sistema de envios v2/
├── WebAPI/
│   ├── WebAPI/              # API Controllers y configuración
│   ├── LogicaAplicacion/    # Casos de uso
│   ├── LogicaNegocio/       # Entidades y Value Objects
│   ├── LogicaAccesoDatos/   # Repositorios y EF Core
│   └── Compartido/          # DTOs y Mappers
│
└── MVC/
    ├── Controllers/         # Controladores MVC
    ├── Models/             # ViewModels
    ├── Views/              # Vistas Razor
    └── wwwroot/            # Recursos estáticos
```

## 🔐 Autenticación

El sistema utiliza JWT (JSON Web Tokens) para la autenticación. La WebAPI valida los tokens en los endpoints protegidos.

### Flujo de Autenticación:
1. El usuario inicia sesión desde la aplicación MVC
2. La MVC envía credenciales a la WebAPI
3. La WebAPI valida y genera un token JWT
4. El token se almacena en la sesión de la MVC
5. Las peticiones subsecuentes incluyen el token en el header

## 🗄️ Modelo de Datos

### Entidades Principales:
- **Envio**: Clase base para envíos (común y urgente)
- **EnvioComun**: Envío estándar
- **EnvioUrgente**: Envío con prioridad
- **Usuario**: Clientes y empleados
- **Comentario**: Comentarios en envíos
- **Agencia**: Agencias de envío
- **Auditoria**: Registro de auditoría

### Value Objects:
- **NroTracking**: Número de seguimiento único
- **Peso**: Peso del envío
- **Estado**: Estado del envío (En proceso, Finalizado)

## 📝 Endpoints de la API

### Envíos
- `GET /api/Envio/{tracking}` - Obtener envío por tracking
- `GET /api/Envio/email/{email}` - Listar envíos de cliente
- `GET /api/Envio/email/{email}/envio/{id}` - Obtener comentarios de envío
- `GET /api/Envio/email/{email}/desde/{desde}/hasta/{hasta}/estado/{estado}` - Filtrar por fecha
- `GET /api/Envio/email/{email}/palabra/{palabra}` - Filtrar por comentario

### Usuarios
- `POST /api/Usuario/login` - Iniciar sesión
- `POST /api/Usuario/cambioPassword` - Cambiar contraseña

## 🎨 Interfaz de Usuario

La aplicación MVC proporciona las siguientes vistas:

- **Login y Gestión de Usuario**
- **Búsqueda por Tracking**
- **Listado de Envíos**
- **Detalle de Envío**
- **Filtros de Envíos**
- **Gestión de Comentarios**

## 🧪 Desarrollo

### Restaurar paquetes NuGet:

```powershell
cd WebAPI
dotnet restore

cd ..\MVC
dotnet restore
```

### Compilar el proyecto:

```powershell
# WebAPI
cd WebAPI\WebAPI
dotnet build

# MVC
cd ..\..\MVC
dotnet build
```

## 📄 Licencia

Este proyecto es parte de un trabajo académico (Obligatorio de Programación 3).

## 👤 Autor

**Matías Pietrafesa**

---

## 📌 Notas

- La aplicación MVC debe ejecutarse después de iniciar la WebAPI
- Asegúrate de que los puertos configurados no estén en uso
- Las migraciones deben aplicarse antes del primer uso
- Para producción, actualizar las cadenas de conexión y configuraciones de seguridad

## 🔧 Configuración Adicional

### Para despliegue en Azure:

El proyecto incluye perfiles de publicación para Azure Web Apps. Las cadenas de conexión comentadas en `appsettings.json` muestran configuraciones de ejemplo para Azure SQL Database.

