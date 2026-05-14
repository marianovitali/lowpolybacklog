# 🎮 Low Poly Backlog API
RESTful API para gestionar y hacer seguimiento de un backlog personal de videojuegos retro, inspirada en la era PlayStation 1.
<div align="center">
  <img src="https://img.shields.io/badge/.NET_10-5C2D91?style=for-the-badge&logo=.net&logoColor=white" alt=".NET 10" />
  <img src="https://img.shields.io/badge/C%23_14-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/Entity_Framework-0078D4?style=for-the-badge&logo=.net&logoColor=white" alt="Entity Framework Core" />
  <img src="https://img.shields.io/badge/Cloudinary-3448C5?style=for-the-badge&logo=Cloudinary&logoColor=white" alt="Cloudinary" />
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=Swagger&logoColor=black" alt="Swagger" />
</div>
<br/>

##  Overview
Low Poly Backlog permite catalogar juegos retro, organizarlos y trackear el progreso de cada uno (estado, horas jugadas, rating y notas personales).
El proyecto está diseñado como una API backend escalable, aplicando buenas prácticas de arquitectura y desarrollo en .NET.

##  Arquitectura y Diseño

La aplicación sigue una arquitectura en capas, separando responsabilidades para mejorar mantenibilidad y testeo:

```
Controllers → Services → Repositories → DbContext → Database
     ↓           ↓            ↓
    DTOs    ← AutoMapper ←  Entities
```

**Decisiones clave:**
- **Service Layer**: centraliza la lógica de negocio y evita lógica en controllers
- **Repository Pattern**: desacopla el acceso a datos
- **DTOs**: separan los contratos de la API del modelo de dominio
- **AutoMapper**: simplifica el mapeo entre capas
- **Entity Framework Core**: manejo de relaciones complejas (Many-to-Many y One-to-One)
- **Paginación y filtrado dinámico**: endpoints preparados para escalar
- **Consumo de APIs externas**: Usando HttpClientFactory

##  Stack Tecnológico

- .NET 10 / C# 14.0
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- Swagger / OpenAPI
- Integración con APIs externas: Cloudinary SDK, IGDB API

##  Funcionalidades

###  Games
- CRUD completo
- Filtros por título, género y año con paginación configurable
- Subida de carátulas de juegos integrando el servicio en la nube de Cloudinary
- Búsqueda y autocompletado de videojuegos mediante integración con la API de IGDB

###  Backlog
- Estados: Pending, Playing, Completed, Abandoned
- Rating (1–10) con validación
- Registro de horas jugadas y notas personales

###  Seguridad
- Autenticación mediante esquema de API Key configurado e integrado directamente en la interfaz de Swagger.

##  Ejemplo de uso

**Request:**
```
GET /api/games?title=metal&pageNumber=1&pageSize=10
```

**Response (ejemplo):**
```json
{
  "items": [
    {
      "id": 1,
      "title": "Metal Gear Solid",
      "year": 1998,
      "genres": ["Stealth", "Action"],
      "backlog": {
        "status": "Completed",
        "rating": 10,
        "hoursPlayed": 15
      }
    }
  ],
  "currentPage": 1,
  "totalPages": 3
}
```

##  Instalación local

Para clonar y ejecutar este proyecto localmente, necesitas configurar tus propias credenciales usando la herramienta de Secretos de Usuario de .NET.

```bash
git clone [https://github.com/marianovitali/lowpolybacklog.git](https://github.com/marianovitali/lowpolybacklog.git)
cd lowpolybacklog

# 1. Inicializar los secretos de usuario en el proyecto
dotnet user-secrets init

# 2. Configurar la API Key de seguridad local
dotnet user-secrets set "ApiKey" "TU_API_KEY_PERSONAL"

# 3. Configurar tus credenciales de Cloudinary
dotnet user-secrets set "CloudinarySettings:ApiKey" "TU_CLOUDINARY_API_KEY"
dotnet user-secrets set "CloudinarySettings:ApiSecret" "TU_CLOUDINARY_API_SECRET"

# 4. Configurar credenciales de IGDB / Twitch

dotnet user-secrets set "IGDB:ClientId" "TU_CLIENT_ID"
dotnet user-secrets set "IGDB:ClientSecret" "TU_CLIENT_SECRET"

# 5. Actualizar la base de datos local
dotnet ef database update

# 6. Ejecutar la API
dotnet run
```

Swagger disponible en: `https://localhost:[port]/swagger`

##  Próximas mejoras

[ ] Manejo global de excepciones (Global Exception Handler).

[ ] Logging estructurado.

[ ] Caching para mejorar performance en consultas frecuentes.

---

**👨‍💻 Autor:** Mariano Vitali | [GitHub](https://github.com/marianovitali)
