//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class ClienteApiConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad ClienteApi
/// </summary>
public class ClienteApiConfiguration : IEntityTypeConfiguration<ClienteApi>
{
    public void Configure(EntityTypeBuilder<ClienteApi> builder)
    {
        // Nombre de tabla
        builder.ToTable("clientes_api");

        // Llave primaria
        builder.HasKey(ca => ca.Id);

        // Propiedades
        builder.Property(ca => ca.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(ca => ca.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ca => ca.Descripcion)
            .HasColumnName("descripcion")
            .HasColumnType("text");

        builder.Property(ca => ca.ClientId)
            .HasColumnName("client_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ca => ca.ClientSecretHash)
            .HasColumnName("client_secret_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(ca => ca.AllowedOrigins)
            .HasColumnName("allowed_origins")
            .HasColumnType("text[]"); // Array de texto en PostgreSQL

        builder.Property(ca => ca.AllowedIps)
            .HasColumnName("allowed_ips")
            .HasColumnType("text[]");

        builder.Property(ca => ca.RateLimit)
            .HasColumnName("rate_limit")
            .IsRequired()
            .HasDefaultValue(1000);

        builder.Property(ca => ca.Scopes)
            .HasColumnName("scopes")
            .HasColumnType("text[]");

        builder.Property(ca => ca.Activo)
            .HasColumnName("activo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(ca => ca.FechaExpiracion)
            .HasColumnName("fecha_expiracion")
            .HasColumnType("timestamp");

        builder.Property(ca => ca.CreadoPor)
            .HasColumnName("creado_por");

        builder.Property(ca => ca.UltimoUso)
            .HasColumnName("ultimo_uso")
            .HasColumnType("timestamp");

        builder.Property(ca => ca.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(ca => ca.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(ca => ca.ClientId)
            .IsUnique()
            .HasDatabaseName("idx_cliente_api_client_id");

        builder.HasIndex(ca => ca.Activo)
            .HasDatabaseName("idx_cliente_api_activo");

        // Relaciones
        builder.HasMany(ca => ca.Logs)
            .WithOne(cal => cal.ClienteApi)
            .HasForeignKey(cal => cal.ClienteApiId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ca => ca.Creador)
            .WithMany()
            .HasForeignKey(ca => ca.CreadoPor)
            .OnDelete(DeleteBehavior.SetNull);
    }
}