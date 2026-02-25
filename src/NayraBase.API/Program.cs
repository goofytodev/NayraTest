using Microsoft.EntityFrameworkCore;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Data.Seeds;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NayraBase.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using NayraBase.Application.Services.Implementations;
using NayraBase.Application.Services.Interfaces;
using NayraBase.Application.Validators.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// ===== CONFIGURAR NPGSQL PARA USAR TIMESTAMP SIN TIME ZONE =====
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// ================================================================

// Add services to the container
builder.Services.AddDbContext<NayraBaseDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsAssembly("NayraBase.Infrastructure")
    );
});

// ===== REGISTRAR UNIT OF WORK =====
builder.Services.AddScoped<IUnitOfWork, NayraBase.Infrastructure.UnitOfWork.UnitOfWork>();
// ==================================

// ===== REGISTRAR SERVICIOS DE APPLICATION =====
builder.Services.AddScoped<IAuthService, AuthService>();

// ===== REGISTRAR VALIDATORS (FluentValidation) =====
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

// Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)
        ),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ===== CONFIGURAR SWAGGER =====
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "NayraBase API",
        Version = "v1.0",
        Description = "API de gestión con RBAC jerárquico"
    });

    // Configurar JWT en Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
// ==============================

var app = builder.Build();

// ===== SEED DE DATOS INICIALES =====
Console.WriteLine("🔄 Iniciando proceso de seed...");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        Console.WriteLine("📦 Obteniendo DbContext...");
        var context = services.GetRequiredService<NayraBaseDbContext>();

        Console.WriteLine("🔄 Aplicando migraciones pendientes...");
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Migraciones aplicadas");

        Console.WriteLine("🌱 Iniciando seed de datos...");
        await context.SeedDatabaseAsync();
        Console.WriteLine("✅ Base de datos inicializada correctamente");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al inicializar la base de datos:");
        Console.WriteLine($"   Mensaje: {ex.Message}");
        Console.WriteLine($"   StackTrace: {ex.StackTrace}");

        if (ex.InnerException != null)
        {
            Console.WriteLine($"   Inner Exception: {ex.InnerException.Message}");
        }
    }
}

Console.WriteLine("🚀 Iniciando aplicación...");
// ===================================

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Endpoint raíz
app.MapGet("/", () => new
{
    service = "NayraBase API",
    version = "1.0.0",
    status = "Running",
    timestamp = DateTime.UtcNow,
    endpoints = new[] { "/swagger", "/api/health" }
});

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "Healthy",
    timestamp = DateTime.UtcNow
}));

app.Run();