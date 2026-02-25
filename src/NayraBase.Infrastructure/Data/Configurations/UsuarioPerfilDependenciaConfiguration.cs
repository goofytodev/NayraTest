//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class UsuarioPerfilDependenciaConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad UsuarioPerfilDependencia
/// TABLA CENTRAL del modelo RBAC jerárquico
/// </summary>
public class UsuarioPerfilDependenciaConfiguration : IEntityTypeConfiguration<UsuarioPerfilDependencia>
{
    public void Configure(EntityTypeBuilder<UsuarioPerfilDependencia> builder)
    {
        // Nombre de tabla
        builder.ToTable("usuario_perfil_dependencia");

        // Llave primaria
        builder.HasKey(upd => upd.Id);

        // Propiedades
        builder.Property(upd => upd.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(upd => upd.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(upd => upd.PerfilId)
            .HasColumnName("perfil_id")
            .IsRequired();

        builder.Property(upd => upd.DependenciaId)
            .HasColumnName("dependencia_id")
            .IsRequired();

        builder.Property(upd => upd.HeredaASubdependencias)
            .HasColumnName("hereda_a_subdependencias")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(upd => upd.ModoHerenciaId)
            .HasColumnName("modo_herencia_id");

        builder.Property(upd => upd.SoloLectura)
            .HasColumnName("solo_lectura")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(upd => upd.PermisosAdicionales)
            .HasColumnName("permisos_adicionales")
            .HasColumnType("integer[]"); // Array de enteros en PostgreSQL

        builder.Property(upd => upd.PermisosRevocados)
            .HasColumnName("permisos_revocados")
            .HasColumnType("integer[]");

        builder.Property(upd => upd.FechaAsignacion)
            .HasColumnName("fecha_asignacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(upd => upd.FechaFin)
            .HasColumnName("fecha_fin")
            .HasColumnType("timestamp");

        builder.Property(upd => upd.AsignadoPor)
            .HasColumnName("asignado_por");

        builder.Property(upd => upd.MotivoAsignacion)
            .HasColumnName("motivo_asignacion")
            .HasColumnType("text");

        builder.Property(upd => upd.DocumentoSustento)
            .HasColumnName("documento_sustento")
            .HasMaxLength(50);

        builder.Property(upd => upd.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(upd => upd.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(upd => upd.UsuarioId)
            .HasDatabaseName("idx_upd_usuario");

        builder.HasIndex(upd => upd.DependenciaId)
            .HasDatabaseName("idx_upd_dependencia");

        builder.HasIndex(upd => new { upd.UsuarioId, upd.DependenciaId })
            .HasDatabaseName("idx_upd_usuario_dependencia");

        // Índice único compuesto (no repetir misma combinación activa)
        builder.HasIndex(upd => new { upd.UsuarioId, upd.PerfilId, upd.DependenciaId })
            .IsUnique()
            .HasDatabaseName("idx_upd_unique");

        // Relaciones
        builder.HasOne(upd => upd.Usuario)
            .WithMany(u => u.PerfilesDependencias)
            .HasForeignKey(upd => upd.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(upd => upd.Perfil)
            .WithMany(p => p.AsignacionesUsuarios)
            .HasForeignKey(upd => upd.PerfilId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(upd => upd.Dependencia)
            .WithMany(d => d.UsuariosAsignados)
            .HasForeignKey(upd => upd.DependenciaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(upd => upd.ModoHerencia)
            .WithMany(mh => mh.AsignacionesConModo)
            .HasForeignKey(upd => upd.ModoHerenciaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}