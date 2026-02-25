//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class RefreshTokenConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad RefreshToken
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Nombre de tabla
        builder.ToTable("refresh_tokens");

        // Llave primaria
        builder.HasKey(rt => rt.Id);

        // Propiedades
        builder.Property(rt => rt.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(rt => rt.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(rt => rt.Token)
            .HasColumnName("token")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(rt => rt.FechaExpiracion)
            .HasColumnName("fecha_expiracion")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(rt => rt.Revocado)
            .HasColumnName("revocado")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(rt => rt.FechaRevocacion)
            .HasColumnName("fecha_revocacion")
            .HasColumnType("timestamp");

        builder.Property(rt => rt.ReemplazadoPor)
            .HasColumnName("reemplazado_por")
            .HasMaxLength(500);

        builder.Property(rt => rt.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(45);

        builder.Property(rt => rt.UserAgent)
            .HasColumnName("user_agent")
            .HasColumnType("text");

        builder.Property(rt => rt.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(rt => rt.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Ignorar propiedades calculadas (no se mapean a BD)
        builder.Ignore(rt => rt.EstaActivo);
        builder.Ignore(rt => rt.EstaExpirado);

        // Índices
        builder.HasIndex(rt => rt.Token)
            .IsUnique()
            .HasDatabaseName("idx_refresh_token");

        builder.HasIndex(rt => rt.UsuarioId)
            .HasDatabaseName("idx_refresh_token_usuario");

        // Relaciones
        builder.HasOne(rt => rt.Usuario)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}