namespace NayraBase.Application.DTOs.Usuario;

/// <summary>
/// DTO para actualizar un usuario existente
/// </summary>
public class UpdateUsuarioDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public bool Activo { get; set; }
    public bool LockoutEnabled { get; set; }
}