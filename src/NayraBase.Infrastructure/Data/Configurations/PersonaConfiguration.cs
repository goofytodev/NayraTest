//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class PersonaConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad Persona
/// </summary>
public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        // Nombre de tabla
        builder.ToTable("personas");

        // Llave primaria
        builder.HasKey(p => p.Id);

        // Propiedades
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nombres)
            .HasColumnName("nombres")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ApellidoPaterno)
            .HasColumnName("apellido_paterno")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ApellidoMaterno)
            .HasColumnName("apellido_materno")
            .HasMaxLength(100);

        builder.Property(p => p.TipoDocumento)
            .HasColumnName("tipo_documento")
            .IsRequired()
            .HasConversion<int>(); // Enum a int

        builder.Property(p => p.NumeroDocumento)
            .HasColumnName("numero_documento")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.FechaNacimiento)
            .HasColumnName("fecha_nacimiento")
            .HasColumnType("date");

        builder.Property(p => p.Genero)
            .HasColumnName("genero")
            .HasConversion<int?>(); // Enum nullable a int

        builder.Property(p => p.Telefono)
            .HasColumnName("telefono")
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasMaxLength(100);

        builder.Property(p => p.Direccion)
            .HasColumnName("direccion")
            .HasColumnType("text");

        builder.Property(p => p.Estado)
            .HasColumnName("estado")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(p => p.NumeroDocumento)
            .IsUnique()
            .HasDatabaseName("idx_persona_numero_documento");

        builder.HasIndex(p => p.Email)
            .HasDatabaseName("idx_persona_email");

        // Relaciones
        builder.HasOne(p => p.Usuario)
            .WithOne(u => u.Persona)
            .HasForeignKey<Usuario>(u => u.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}