using Microsoft.EntityFrameworkCore;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Security;

namespace NayraBase.Infrastructure.Data.Seeds;

/// <summary>
/// Script para actualizar el password del usuario admin
/// </summary>
public static class UpdateAdminPassword
{
    /// <summary>
    /// Actualiza el password del usuario admin
    /// </summary>
    /// <param name="context">DbContext</param>
    /// <param name="newPassword">Nueva contraseña (por defecto: Admin123!)</param>
    public static async Task UpdateAsync(NayraBaseDbContext context, string newPassword = "Admin123!")
    {
        Console.WriteLine("   🔐 Actualizando password del usuario admin...");

        // Buscar usuario admin
        var admin = await context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == "admin");

        if (admin == null)
        {
            Console.WriteLine("   ⚠️  Usuario admin no encontrado");
            return;
        }

        // Verificar si ya tiene un hash válido (no temporal)
        if (admin.PasswordHash != "TEMPORAL_HASH_CAMBIAR_DESPUES")
        {
            Console.WriteLine("   ℹ️  El usuario admin ya tiene un password configurado");
            return;
        }

        // Crear instancia de PasswordHasher
        var passwordHasher = new PasswordHasher();

        // Hashear la nueva contraseña
        var passwordHash = passwordHasher.HashPassword(newPassword);

        // Actualizar usuario
        admin.PasswordHash = passwordHash;
        admin.SecurityStamp = SecurityStampGenerator.Generate();
        admin.ConcurrencyStamp = SecurityStampGenerator.GenerateConcurrencyStamp();
        admin.DebeCambiarPassword = false; // Ya configuramos un password inicial

        await context.SaveChangesAsync();

        Console.WriteLine("   ✅ Password del admin actualizado correctamente");
        Console.WriteLine($"   📧 Username: {admin.Username}");
        Console.WriteLine($"   🔑 Password: {newPassword}");
    }
}