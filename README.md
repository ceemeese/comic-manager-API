# Comic Manager: API para gestión de comics, usuarios, géneros

Este proyecto es una API para gestionar una colección de cómics, usuarios, géneros y listas de cómics de usuarios. El proyecto está compuesto por un **Backend** separado por capas

## Tecnologías utilizadas
- Backend: ![Static Badge](https://img.shields.io/badge/-nodejs?style=flat&logo=dotnet&logoColor=%23512BD4&label=.NET&labelColor=FFFFFF&color=FFFFFF)
- ORM: ![Static Badge](https://img.shields.io/badge/-EntityFramework?style=flat&logo=dotnet&logoColor=%23512BD4&label=EntityFramework&labelColor=FFFFFF&color=FFFFFF)  
- Base de datos: ![Static Badge](https://img.shields.io/badge/-mysql?style=flat&logo=mysql&logoColor=%23003545&label=MySQL&labelColor=FFFFFF&color=FFFFFF)
- Documentación interactiva de la API: ![Static Badge](https://img.shields.io/badge/-swagger?style=flat&logo=swagger&logoColor=%2385EA2D&label=Swagger&labelColor=FFFFFF&color=FFFFFF)
- Autenticación y autorización: ![Static Badge](https://img.shields.io/badge/-jwt?style=flat&logo=jsonwebtokens&logoColor=%23000000&label=JWT&labelColor=FFFFFF&color=FFFFFF)
- Contenerización: ![Static Badge](https://img.shields.io/badge/-docker?style=flat&logo=docker&logoColor=%232496ED&label=Docker&labelColor=FFFFFF&color=FFFFFF)


## Arquitectura
La API sigue una arquitectura RESTful y está separada por capas.

**Backend API:** Servidor en ASP.NET Core Web API (C#), que gestiona toda la lógica de negocio y expone endpoints RESTful.

**Base de datos:** Base de datos relacional MySQL. Se gestiona mediante Entity Framework Core y migraciones.


## Primeros pasos 
### Requisitos previos
- Docker y Docker Compose
- .NET 8 SDK

### Clonar repositorio
```
git clone https://github.com/ceemeese/comic-manager-api.git
cd comic-manager-api
```

### Ejecutar la aplicación con Docker
Esta es la forma recomendada de ejecutar toda la aplicación. Construir e iniciar todos los servicios:
```
docker-compose up --build
```

La aplicación estará disponible en las siguientes ubicaciones:
- API del Backend: http://localhost:7877
- Base de datos: localhost:7787
- Swagger: http://localhost:7877/swagger

### Desarrollo en local
También se puede ejecutar el backend en local durante el desarrollo
En ese caso el comando de ejecución 
```
dotnet run --project API
```
> **Nota:** Para desarrollo local necesitarás configurar MySQL por separado o usar el contenedor de base de datos


## Roles y permisos
### 👤 Usuario público (sin autenticación)
#### Cómics
- Consultar listado completo de cómics
- Ver detalles de un cómic específico
- Buscar cómics por filtros
#### Géneros
- Consultar listado de géneros disponibles
- Ver detalles de un género específico
#### Relaciones cómic-género:
- Ver géneros asociados a un cómic
- Ver cómics de un género específico
- Consultar relaciones específicas entre cómic y género


### 🔑 Usuario registrado
- Todo lo del usuario público
- Gestión de lista personal
  - Añadir cómics a su lista personal
  - Eliminar cómics de su lista personal
  - Ver su lista de cómics
  - Actualizar estado/información de cómics en su lista
- Consultas de listas:
  - Ver otros usuarios que tienen un cómic específico
  - Consultar relaciones usuario-cómic específicas
- Perfil:
  - Actualizar su propio perfil de usuario


### 👑 Administrador
- Todas las funcionalidades anteriores
- Gestión completa de cómics:
  - Crear nuevos cómics
  - Editar información de cómics existentes
  - Eliminar cómics del sistema
- Gestión completa de géneros:
  - Crear nuevos géneros
  - Editar géneros existentes
  - Eliminar géneros del sistema
- Gestión de relaciones cómic-género:
  - Crear asociaciones entre cómics y géneros
  - Eliminar asociaciones cómic-género
- Administración de usuarios:
  - Ver listado completo de usuarios
  - Consultar detalles de cualquier usuario
  - Eliminar usuarios del sistema
- Acceso total al sistema

> **Nota:**  Los usuarios registrados solo pueden gestionar su propia lista de cómics y actualizar su propio perfil. El admin tiene control total sobre todos los recursos del sistema












