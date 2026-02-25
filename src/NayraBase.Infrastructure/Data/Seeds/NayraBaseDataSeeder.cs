using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Domain.Enums;
using NayraBase.Infrastructure.Data.Context;
using System.Runtime.ConstrainedExecution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NayraBase.Infrastructure.Data.Seeds;

/// <summary>
/// Clase para sembrar datos iniciales en la base de datos
/// </summary>
public static class NayraBaseDataSeeder
{
    /// <summary>
    /// Siembra los datos iniciales en la base de datos
    /// </summary>
    public static async Task SeedAsync(NayraBaseDbContext context)
    {
        Console.WriteLine("   🔍 Verificando si ya existen datos...");

        // Verificar si ya existen datos
        if (await context.Usuarios.AnyAsync())
        {
            Console.WriteLine("   ℹ️  Los datos ya existen, omitiendo seed");
            await UpdateAdminPassword.UpdateAsync(context);
            return;
        }

        Console.WriteLine("   📝 Sembrando Modos de Herencia...");
        await SeedModosHerenciaAsync(context);

        Console.WriteLine("   📝 Sembrando Permisos...");
        await SeedPermisosAsync(context);

        Console.WriteLine("   📝 Sembrando Perfiles...");
        await SeedPerfilesAsync(context);

        Console.WriteLine("   📝 Sembrando Dependencia Raíz...");
        await SeedDependenciaRaizAsync(context);

        Console.WriteLine("   📝 Sembrando Usuario SuperAdmin...");
        await SeedUsuarioSuperAdminAsync(context);

        Console.WriteLine("   💾 Guardando cambios...");
        await UpdateAdminPassword.UpdateAsync(context);
        await context.SaveChangesAsync();
        Console.WriteLine("   ✅ Datos sembrados exitosamente");
    }

    #region Modos de Herencia

    private static async Task SeedModosHerenciaAsync(NayraBaseDbContext context)
    {
        var modos = new List<ModoHerencia>
        {
            new ModoHerencia
            {
                Codigo = "SUPERVISION",
                Nombre = "Supervisión",
                Descripcion = "Permite ver y generar reportes de subdependencias sin modificar datos operativos",
                PermiteOperaciones = false,
                PermiteLectura = true,
                PermiteReportes = true,
                PermiteAprobaciones = false,
                ColorHex = "#3B82F6",
                Icono = "eye",
                NivelAcceso = 3,
                EsSistema = true,
                Activo = true,
                FiltroPermisos = "{\"incluir\": [\"es_supervisión\", \"es_lectura\"], \"excluir\": [\"es_operativo\"]}"
            },
            new ModoHerencia
            {
                Codigo = "OPERATIVO",
                Nombre = "Operativo Heredado",
                Descripcion = "Permite operaciones completas en subdependencias (usar con precaución)",
                PermiteOperaciones = true,
                PermiteLectura = true,
                PermiteReportes = true,
                PermiteAprobaciones = false,
                ColorHex = "#EF4444",
                Icono = "edit",
                NivelAcceso = 4,
                EsSistema = true,
                Activo = true,
                FiltroPermisos = "{\"incluir\": [\"es_operativo\", \"es_lectura\"], \"excluir\": []}"
            },
            new ModoHerencia
            {
                Codigo = "AUDITORIA",
                Nombre = "Auditoría",
                Descripcion = "Acceso completo de solo lectura para fines de auditoría y fiscalización",
                PermiteOperaciones = false,
                PermiteLectura = true,
                PermiteReportes = true,
                PermiteAprobaciones = false,
                ColorHex = "#8B5CF6",
                Icono = "shield",
                NivelAcceso = 5,
                EsSistema = true,
                Activo = true,
                FiltroPermisos = "{\"incluir\": [\"es_auditoria\", \"es_lectura\"], \"excluir\": [\"es_operativo\"]}"
            },
            new ModoHerencia
            {
                Codigo = "APROBACION",
                Nombre = "Aprobación",
                Descripcion = "Solo puede aprobar/rechazar documentos, sin modificar contenido",
                PermiteOperaciones = false,
                PermiteLectura = true,
                PermiteReportes = true,
                PermiteAprobaciones = true,
                ColorHex = "#10B981",
                Icono = "check-circle",
                NivelAcceso = 3,
                EsSistema = true,
                Activo = true,
                FiltroPermisos = "{\"incluir\": [\"es_aprobación\", \"es_lectura\"], \"excluir\": [\"es_operativo\"]}"
            },
            new ModoHerencia
            {
                Codigo = "CONSULTA",
                Nombre = "Consulta Simple",
                Descripcion = "Solo lectura sin capacidad de generar reportes ni aprobar",
                PermiteOperaciones = false,
                PermiteLectura = true,
                PermiteReportes = false,
                PermiteAprobaciones = false,
                ColorHex = "#6B7280",
                Icono = "search",
                NivelAcceso = 1,
                EsSistema = true,
                Activo = true,
                FiltroPermisos = "{\"incluir\": [\"es_lectura\"], \"excluir\": [\"es_operativo\"]}"
            }
        };

        await context.ModosHerencia.AddRangeAsync(modos);
    }

    #endregion

    #region Permisos

    private static async Task SeedPermisosAsync(NayraBaseDbContext context)
    {
        var permisos = new List<Permiso>
        {
            // USUARIOS
            new Permiso
            {
                Codigo = "USUARIOS_CREAR",
                Modulo = "Usuarios",
                Recurso = "Usuario",
                Accion = "Crear",
                Alcance = "Dependencia",
                EsOperativo = true,
                Descripcion = "Crear nuevos usuarios",
                NivelSensibilidad = 3,
                EsSistema = true
            },
            new Permiso
            {
                Codigo = "USUARIOS_LEER",
                Modulo = "Usuarios",
                Recurso = "Usuario",
                Accion = "Leer",
                Alcance = "Dependencia",
                EsSupervision = true,
                Descripcion = "Ver usuarios",
                NivelSensibilidad = 2,
                EsSistema = true
            },
            new Permiso
            {
                Codigo = "USUARIOS_MODIFICAR",
                Modulo = "Usuarios",
                Recurso = "Usuario",
                Accion = "Modificar",
                Alcance = "Dependencia",
                EsOperativo = true,
                Descripcion = "Modificar usuarios",
                NivelSensibilidad = 3,
                EsSistema = true
            },
            new Permiso
            {
                Codigo = "USUARIOS_ELIMINAR",
                Modulo = "Usuarios",
                Recurso = "Usuario",
                Accion = "Eliminar",
                Alcance = "Dependencia",
                EsOperativo = true,
                Descripcion = "Eliminar usuarios",
                RequiereJustificacion = true,
                NivelSensibilidad = 4,
                EsSistema = true
            },
            
            // DEPENDENCIAS
            new Permiso
            {
                Codigo = "DEPENDENCIAS_CREAR",
                Modulo = "Dependencias",
                Recurso = "Dependencia",
                Accion = "Crear",
                Alcance = "Subdependencias",
                EsOperativo = true,
                Descripcion = "Crear subdependencias",
                NivelSensibilidad = 3,
                EsSistema = true
            },
            new Permiso
            {
                Codigo = "DEPENDENCIAS_LEER",
                Modulo = "Dependencias",
                Recurso = "Dependencia",
                Accion = "Leer",
                Alcance = "Todo",
                EsSupervision = true,
                Descripcion = "Ver dependencias",
                NivelSensibilidad = 1,
                EsSistema = true
            },
            new Permiso
            {
                Codigo = "DEPENDENCIAS_MODIFICAR",
                Modulo = "Dependencias",
                Recurso = "Dependencia",
                Accion = "Modificar",
                Alcance = "Subdependencias",
                EsOperativo = true,
                Descripcion = "Modificar dependencias",
                NivelSensibilidad = 3,
                EsSistema = true
            },
            
            // PERFILES Y PERMISOS
            new Permiso
            {
                Codigo = "PERFILES_GESTIONAR",
                Modulo = "Perfiles",
                Recurso = "Perfil",
                Accion = "Gestionar",
                Alcance = "Todo",
                EsOperativo = true,
                Descripcion = "Gestionar perfiles",
                NivelSensibilidad = 4,
                EsSistema = true
            },
            new Permiso
            {
                Codigo = "PERMISOS_GESTIONAR",
                Modulo = "Permisos",
                Recurso = "Permiso",
                Accion = "Gestionar",
                Alcance = "Todo",
                EsOperativo = true,
                Descripcion = "Gestionar permisos",
                NivelSensibilidad = 4,
                EsSistema = true
            },
            
            // AUDITORÍA
            new Permiso
            {
                Codigo = "AUDITORIA_LEER",
                Modulo = "Auditoría",
                Recurso = "Log",
                Accion = "Leer",
                Alcance = "Todo",
                EsAuditoria = true,
                Descripcion = "Ver logs de auditoría",
                NivelSensibilidad = 4,
                EsSistema = true
            }
        };

        await context.Permisos.AddRangeAsync(permisos);
    }

    #endregion

    #region Perfiles

    private static async Task SeedPerfilesAsync(NayraBaseDbContext context)
    {
        var perfiles = new List<Perfil>
        {
            new Perfil
            {
                Nombre = "SuperAdmin",
                Descripcion = "Administrador con acceso total al sistema",
                NivelJerarquico = 1,
                EsOperativo = true,
                EsSupervision = true
            },
            new Perfil
            {
                Nombre = "Administrador",
                Descripcion = "Administrador de dependencia",
                NivelJerarquico = 2,
                EsOperativo = true,
                EsSupervision = true
            },
            new Perfil
            {
                Nombre = "Jefe de Área",
                Descripcion = "Jefe con supervisión",
                NivelJerarquico = 3,
                EsSupervision = true
            },
            new Perfil
            {
                Nombre = "Supervisor",
                Descripcion = "Supervisor con lectura y reportes",
                NivelJerarquico = 4,
                EsSupervision = true
            },
            new Perfil
            {
                Nombre = "Usuario Operativo",
                Descripcion = "Usuario con permisos operativos",
                NivelJerarquico = 5,
                EsOperativo = true
            },
            new Perfil
            {
                Nombre = "Auditor",
                Descripcion = "Auditor con acceso de solo lectura",
                NivelJerarquico = 2
            }
        };

        await context.Perfiles.AddRangeAsync(perfiles);
    }

    #endregion

    #region Dependencia Raíz

    private static async Task SeedDependenciaRaizAsync(NayraBaseDbContext context)
    {
        var dependenciaRaiz = new Dependencia
        {
            Nombre = "Organización",
            Codigo = "ORG-ROOT",
            Descripcion = "Dependencia raíz de la organización",
            DependenciaPadreId = null,
            Nivel = 0,
            RutaCompleta = "Organización",
            EsHoja = true,
            Orden = 0,
            Estado = true
        };

        await context.Dependencias.AddAsync(dependenciaRaiz);
    }

    #endregion

    #region Usuario SuperAdmin

    private static async Task SeedUsuarioSuperAdminAsync(NayraBaseDbContext context)
    {
        // Crear persona
        var personaAdmin = new Persona
        {
            Nombres = "Administrador",
            ApellidoPaterno = "Sistema",
            ApellidoMaterno = "NayraBase",
            TipoDocumento = TipoDocumento.DNI,
            NumeroDocumento = "00000000",
            Email = "admin@nayrabase.com",
            Estado = true
        };

        await context.Personas.AddAsync(personaAdmin);
        await context.SaveChangesAsync();

        // Crear usuario (password temporal: "Admin123!")
        var usuarioAdmin = new Usuario
        {
            PersonaId = personaAdmin.Id,
            Username = "admin",
            NormalizedUsername = "ADMIN",
            Email = "admin@nayrabase.com",
            NormalizedEmail = "ADMIN@NAYRABASE.COM",
            EmailConfirmed = true,
            // Password hash temporal - se actualizará después
            PasswordHash = "TEMPORAL_HASH_CAMBIAR_DESPUES",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            LockoutEnabled = false,
            DebeCambiarPassword = true,
            Activo = true
        };

        await context.Usuarios.AddAsync(usuarioAdmin);
    }

    #endregion
}