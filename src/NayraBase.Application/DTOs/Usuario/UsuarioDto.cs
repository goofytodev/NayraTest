namespace NayraBase.Application.DTOs.Usuario;

/// <summary>
/// DTO completo de usuario
/// </summary>
public class UsuarioDto
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }
    public bool DebeCambiarPassword { get; set; }
    public DateTime? UltimoAcceso { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Navegación
    public PersonaDto? Persona { get; set; }
}

public class PersonaDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string? Genero { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public bool Estado { get; set; }
}