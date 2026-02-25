namespace NayraBase.Application.DTOs.Auth;

/// <summary>
/// DTO con información básica del usuario autenticado
/// </summary>
public class UsuarioInfoDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public bool DebeCambiarPassword { get; set; }
    public PersonaBasicaDto Persona { get; set; } = null!;
    public List<string> Roles { get; set; } = new();
}

public class PersonaBasicaDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public string NombreCompleto => $"{Nombres} {ApellidoPaterno} {ApellidoMaterno}".Trim();
}