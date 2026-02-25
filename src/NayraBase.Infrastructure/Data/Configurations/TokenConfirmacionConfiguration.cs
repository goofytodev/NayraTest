//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class TokenConfirmacionConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad TokenConfirmacion
/// </summary>
public class TokenConfirmacionConfiguration : IEntityTypeConfiguration<TokenConfirmacion>
{
    public void Configure(EntityTypeBuilder<TokenConfirmacion> builder)
    {
        // Nombre de tabla
        builder.ToTable("tokens_confirmacion");

        // Llave primaria
        builder.HasKey(tc => tc.Id);

        // Propiedades
        builder.Property(tc => tc.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(tc => tc.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(tc => tc.Token)
            .HasColumnName("token")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(tc => tc.Tipo)
            .HasColumnName("tipo")
            .IsRequired()
            .HasConversion<int>(); // Enum a int

        builder.Property(tc => tc.FechaExpiracion)
            .HasColumnName("fecha_expiracion")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(tc => tc.Usado)
            .HasColumnName("usado")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(tc => tc.FechaUso)
            .HasColumnName("fecha_uso")
            .HasColumnType("timestamp");

        builder.Property(tc => tc.IpAddressUso)
            .HasColumnName("ip_address_uso")
            .HasMaxLength(45);

        builder.Property(tc => tc.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(tc => tc.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Ignorar propiedades calculadas
        builder.Ignore(tc => tc.EsValido);
        builder.Ignore(tc => tc.EstaExpirado);

        // Índices
        builder.HasIndex(tc => tc.Token)
            .IsUnique()
            .HasDatabaseName("idx_token_confirmacion");

        builder.HasIndex(tc => new { tc.UsuarioId, tc.Tipo })
            .HasDatabaseName("idx_token_usuario_tipo");

        // Relaciones
        builder.HasOne(tc => tc.Usuario)
            .WithMany(u => u.TokensConfirmacion)
            .HasForeignKey(tc => tc.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}