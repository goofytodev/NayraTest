//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class UsuarioConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad Usuario
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        // Nombre de tabla
        builder.ToTable("usuarios");

        // Llave primaria
        builder.HasKey(u => u.Id);

        // Propiedades
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.PersonaId)
            .HasColumnName("persona_id")
            .IsRequired();

        builder.Property(u => u.Username)
            .HasColumnName("username")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.NormalizedUsername)
            .HasColumnName("normalized_username")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.NormalizedEmail)
            .HasColumnName("normalized_email")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.EmailConfirmed)
            .HasColumnName("email_confirmed")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.SecurityStamp)
            .HasColumnName("security_stamp")
            .HasMaxLength(50)
            .IsRequired()
            .HasDefaultValueSql("gen_random_uuid()::text");

        builder.Property(u => u.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp")
            .HasMaxLength(50)
            .IsRequired()
            .HasDefaultValueSql("gen_random_uuid()::text")
            .IsConcurrencyToken(); // Control de concurrencia

        builder.Property(u => u.LockoutEnabled)
            .HasColumnName("lockout_enabled")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(u => u.LockoutEnd)
            .HasColumnName("lockout_end")
            .HasColumnType("timestamp");

        builder.Property(u => u.AccessFailedCount)
            .HasColumnName("access_failed_count")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(u => u.TwoFactorEnabled)
            .HasColumnName("two_factor_enabled")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20);

        builder.Property(u => u.PhoneNumberConfirmed)
            .HasColumnName("phone_number_confirmed")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.DebeCambiarPassword)
            .HasColumnName("debe_cambiar_password")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(u => u.UltimoAcceso)
            .HasColumnName("ultimo_acceso")
            .HasColumnType("timestamp");

        builder.Property(u => u.Activo)
            .HasColumnName("activo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(u => u.CreadoPor)
            .HasColumnName("creado_por");

        builder.Property(u => u.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(u => u.PersonaId)
            .IsUnique()
            .HasDatabaseName("idx_usuario_persona");

        builder.HasIndex(u => u.NormalizedUsername)
            .IsUnique()
            .HasDatabaseName("idx_usuario_normalized_username");

        builder.HasIndex(u => u.NormalizedEmail)
            .IsUnique()
            .HasDatabaseName("idx_usuario_normalized_email");

        builder.HasIndex(u => u.SecurityStamp)
            .HasDatabaseName("idx_usuario_security_stamp");

        // Relaciones
        builder.HasOne(u => u.Persona)
            .WithOne(p => p.Usuario)
            .HasForeignKey<Usuario>(u => u.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.Usuario)
            .HasForeignKey(rt => rt.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.TokensConfirmacion)
            .WithOne(tc => tc.Usuario)
            .HasForeignKey(tc => tc.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.PerfilesDependencias)
            .WithOne(upd => upd.Usuario)
            .HasForeignKey(upd => upd.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}