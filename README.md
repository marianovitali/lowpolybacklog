# 🎮 Low Poly Backlog API
RESTful API para gestionar y hacer seguimiento de un backlog personal de videojuegos de PlayStation 1.

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

##  Stack Tecnológico

- .NET 10 / C# 14.0
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- Swagger / OpenAPI

##  Funcionalidades

###  Games
- CRUD completo
- Filtros por título, género y año
- Paginación configurable
- Relación Many-to-Many con géneros

###  Backlog
- Estados: Pending, Playing, Completed, Abandoned
- Rating (1–10) con validación
- Registro de horas jugadas
- Notas personales
- Relación One-to-One con juegos

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

## ⚙️ Instalación local

```bash
git clone https://github.com/marianovitali/lowpolybacklog.git
cd lowpolybacklog

# Configurar connection string en appsettings.json

dotnet ef database update
dotnet run
```

Swagger disponible en: `https://localhost:[port]/swagger`

## 🔜 Próximas mejoras

- [ ] Autenticación (API Key / JWT)
- [ ] Integración con API externa (IGDB)
- [ ] Upload de imágenes (Cloudinary)
- [ ] Manejo global de excepciones
- [ ] Logging estructurado
- [ ] Caching para mejorar performance

---

**👨‍💻 Autor:** Mariano Vitali | [GitHub](https://github.com/marianovitali)
