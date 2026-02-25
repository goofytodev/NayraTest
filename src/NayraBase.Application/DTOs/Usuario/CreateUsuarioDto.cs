using NayraBase.Domain.Enums;

namespace NayraBase.Application.DTOs.Usuario;

/// <summary>
/// DTO para crear un nuevo usuario
/// </summary>
public class CreateUsuarioDto
{
    // Datos de Persona
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public Genero? Genero { get; set; }
    public string? Telefono { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Direccion { get; set; }

    // Datos de Usuario
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool DebeCambiarPassword { get; set; } = true;
}