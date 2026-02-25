//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class ClienteApiLogConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad ClienteApiLog
/// </summary>
public class ClienteApiLogConfiguration : IEntityTypeConfiguration<ClienteApiLog>
{
    public void Configure(EntityTypeBuilder<ClienteApiLog> builder)
    {
        // Nombre de tabla
        builder.ToTable("cliente_api_logs");

        // Llave primaria
        builder.HasKey(cal => cal.Id);

        // Propiedades
        builder.Property(cal => cal.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(cal => cal.ClienteApiId)
            .HasColumnName("cliente_api_id")
            .IsRequired();

        builder.Property(cal => cal.Endpoint)
            .HasColumnName("endpoint")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(cal => cal.Metodo)
            .HasColumnName("metodo")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(cal => cal.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(45);

        builder.Property(cal => cal.UserAgent)
            .HasColumnName("user_agent")
            .HasColumnType("text");

        builder.Property(cal => cal.FechaRequest)
            .HasColumnName("fecha_request")
            .HasColumnType("timestamp")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(cal => cal.ResponseCode)
            .HasColumnName("response_code")
            .IsRequired();

        builder.Property(cal => cal.DuracionMs)
            .HasColumnName("duracion_ms");

        builder.Property(cal => cal.ErrorMessage)
            .HasColumnName("error_message")
            .HasColumnType("text");

        builder.Property(cal => cal.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(cal => cal.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(cal => cal.ClienteApiId)
            .HasDatabaseName("idx_log_cliente_api");

        builder.HasIndex(cal => new { cal.ClienteApiId, cal.FechaRequest })
            .HasDatabaseName("idx_log_cliente_fecha");

        builder.HasIndex(cal => cal.ResponseCode)
            .HasDatabaseName("idx_log_response_code");

        // Relaciones
        builder.HasOne(cal => cal.ClienteApi)
            .WithMany(ca => ca.Logs)
            .HasForeignKey(cal => cal.ClienteApiId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}