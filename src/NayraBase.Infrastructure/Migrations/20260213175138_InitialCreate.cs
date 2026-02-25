using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NayraBase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dependencias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    dependencia_padre_id = table.Column<int>(type: "integer", nullable: true),
                    nivel = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ruta_completa = table.Column<string>(type: "text", nullable: true),
                    es_hoja = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dependencias", x => x.id);
                    table.ForeignKey(
                        name: "FK_dependencias_dependencias_dependencia_padre_id",
                        column: x => x.dependencia_padre_id,
                        principalTable: "dependencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "modos_herencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    permite_operaciones = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    permite_lectura = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    permite_reportes = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    permite_aprobaciones = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    filtro_permisos = table.Column<string>(type: "jsonb", nullable: true),
                    color_hex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    icono = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nivel_acceso = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    es_sistema = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_por = table.Column<int>(type: "integer", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modos_herencia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    nivel_jerarquico = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    es_operativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    es_supervision = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    modulo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    recurso = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    accion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    alcance = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    es_operativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    es_supervision = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    es_auditoria = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    requiere_justificacion = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    nivel_sensibilidad = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    es_sistema = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_paterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_materno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tipo_documento = table.Column<int>(type: "integer", nullable: false),
                    numero_documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    genero = table.Column<int>(type: "integer", nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    direccion = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dependencia_perfil_config",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dependencia_id = table.Column<int>(type: "integer", nullable: false),
                    perfil_id = table.Column<int>(type: "integer", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    cantidad_maxima = table.Column<int>(type: "integer", nullable: true),
                    requiere_aprobacion = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    configurado_por = table.Column<int>(type: "integer", nullable: true),
                    fecha_configuracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dependencia_perfil_config", x => x.id);
                    table.ForeignKey(
                        name: "FK_dependencia_perfil_config_dependencias_dependencia_id",
                        column: x => x.dependencia_id,
                        principalTable: "dependencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dependencia_perfil_config_perfiles_perfil_id",
                        column: x => x.perfil_id,
                        principalTable: "perfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "perfil_permiso",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    perfil_id = table.Column<int>(type: "integer", nullable: false),
                    permiso_id = table.Column<int>(type: "integer", nullable: false),
                    puede_delegar = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfil_permiso", x => x.id);
                    table.ForeignKey(
                        name: "FK_perfil_permiso_perfiles_perfil_id",
                        column: x => x.perfil_id,
                        principalTable: "perfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_perfil_permiso_permisos_permiso_id",
                        column: x => x.permiso_id,
                        principalTable: "permisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    normalized_username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    normalized_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    security_stamp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "gen_random_uuid()::text"),
                    concurrency_stamp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "gen_random_uuid()::text"),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    lockout_end = table.Column<DateTime>(type: "timestamp", nullable: true),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    debe_cambiar_password = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ultimo_acceso = table.Column<DateTime>(type: "timestamp", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_por = table.Column<int>(type: "integer", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes_api",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    client_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    client_secret_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    allowed_origins = table.Column<string[]>(type: "text[]", nullable: true),
                    allowed_ips = table.Column<string[]>(type: "text[]", nullable: true),
                    rate_limit = table.Column<int>(type: "integer", nullable: false, defaultValue: 1000),
                    scopes = table.Column<string[]>(type: "text[]", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_expiracion = table.Column<DateTime>(type: "timestamp", nullable: true),
                    creado_por = table.Column<int>(type: "integer", nullable: true),
                    ultimo_uso = table.Column<DateTime>(type: "timestamp", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes_api", x => x.id);
                    table.ForeignKey(
                        name: "FK_clientes_api_usuarios_creado_por",
                        column: x => x.creado_por,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    fecha_expiracion = table.Column<DateTime>(type: "timestamp", nullable: false),
                    revocado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    fecha_revocacion = table.Column<DateTime>(type: "timestamp", nullable: true),
                    reemplazado_por = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ip_address = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens_confirmacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    fecha_expiracion = table.Column<DateTime>(type: "timestamp", nullable: false),
                    usado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    fecha_uso = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ip_address_uso = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens_confirmacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_tokens_confirmacion_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_perfil_dependencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    perfil_id = table.Column<int>(type: "integer", nullable: false),
                    dependencia_id = table.Column<int>(type: "integer", nullable: false),
                    hereda_a_subdependencias = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    modo_herencia_id = table.Column<int>(type: "integer", nullable: true),
                    solo_lectura = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    permisos_adicionales = table.Column<int[]>(type: "integer[]", nullable: true),
                    permisos_revocados = table.Column<int[]>(type: "integer[]", nullable: true),
                    fecha_asignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_fin = table.Column<DateTime>(type: "timestamp", nullable: true),
                    asignado_por = table.Column<int>(type: "integer", nullable: true),
                    motivo_asignacion = table.Column<string>(type: "text", nullable: true),
                    documento_sustento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_perfil_dependencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuario_perfil_dependencia_dependencias_dependencia_id",
                        column: x => x.dependencia_id,
                        principalTable: "dependencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_perfil_dependencia_modos_herencia_modo_herencia_id",
                        column: x => x.modo_herencia_id,
                        principalTable: "modos_herencia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_usuario_perfil_dependencia_perfiles_perfil_id",
                        column: x => x.perfil_id,
                        principalTable: "perfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_perfil_dependencia_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cliente_api_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cliente_api_id = table.Column<int>(type: "integer", nullable: false),
                    endpoint = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    metodo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ip_address = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "text", nullable: true),
                    fecha_request = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    response_code = table.Column<int>(type: "integer", nullable: false),
                    duracion_ms = table.Column<int>(type: "integer", nullable: true),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente_api_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_cliente_api_logs_clientes_api_cliente_api_id",
                        column: x => x.cliente_api_id,
                        principalTable: "clientes_api",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_log_cliente_api",
                table: "cliente_api_logs",
                column: "cliente_api_id");

            migrationBuilder.CreateIndex(
                name: "idx_log_cliente_fecha",
                table: "cliente_api_logs",
                columns: new[] { "cliente_api_id", "fecha_request" });

            migrationBuilder.CreateIndex(
                name: "idx_log_response_code",
                table: "cliente_api_logs",
                column: "response_code");

            migrationBuilder.CreateIndex(
                name: "idx_cliente_api_activo",
                table: "clientes_api",
                column: "activo");

            migrationBuilder.CreateIndex(
                name: "idx_cliente_api_client_id",
                table: "clientes_api",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clientes_api_creado_por",
                table: "clientes_api",
                column: "creado_por");

            migrationBuilder.CreateIndex(
                name: "idx_dependencia_perfil_unique",
                table: "dependencia_perfil_config",
                columns: new[] { "dependencia_id", "perfil_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dependencia_perfil_config_perfil_id",
                table: "dependencia_perfil_config",
                column: "perfil_id");

            migrationBuilder.CreateIndex(
                name: "idx_dependencia_codigo",
                table: "dependencias",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_dependencia_nivel",
                table: "dependencias",
                column: "nivel");

            migrationBuilder.CreateIndex(
                name: "idx_dependencia_padre",
                table: "dependencias",
                column: "dependencia_padre_id");

            migrationBuilder.CreateIndex(
                name: "idx_modo_herencia_codigo",
                table: "modos_herencia",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_perfil_permiso_unique",
                table: "perfil_permiso",
                columns: new[] { "perfil_id", "permiso_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfil_permiso_permiso_id",
                table: "perfil_permiso",
                column: "permiso_id");

            migrationBuilder.CreateIndex(
                name: "idx_perfil_nombre",
                table: "perfiles",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_permiso_codigo",
                table: "permisos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_permiso_modulo",
                table: "permisos",
                column: "modulo");

            migrationBuilder.CreateIndex(
                name: "idx_permiso_recurso",
                table: "permisos",
                column: "recurso");

            migrationBuilder.CreateIndex(
                name: "idx_persona_email",
                table: "personas",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "idx_persona_numero_documento",
                table: "personas",
                column: "numero_documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_refresh_token",
                table: "refresh_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_refresh_token_usuario",
                table: "refresh_tokens",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "idx_token_confirmacion",
                table: "tokens_confirmacion",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_token_usuario_tipo",
                table: "tokens_confirmacion",
                columns: new[] { "usuario_id", "tipo" });

            migrationBuilder.CreateIndex(
                name: "idx_upd_dependencia",
                table: "usuario_perfil_dependencia",
                column: "dependencia_id");

            migrationBuilder.CreateIndex(
                name: "idx_upd_unique",
                table: "usuario_perfil_dependencia",
                columns: new[] { "usuario_id", "perfil_id", "dependencia_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_upd_usuario",
                table: "usuario_perfil_dependencia",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "idx_upd_usuario_dependencia",
                table: "usuario_perfil_dependencia",
                columns: new[] { "usuario_id", "dependencia_id" });

            migrationBuilder.CreateIndex(
                name: "IX_usuario_perfil_dependencia_modo_herencia_id",
                table: "usuario_perfil_dependencia",
                column: "modo_herencia_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_perfil_dependencia_perfil_id",
                table: "usuario_perfil_dependencia",
                column: "perfil_id");

            migrationBuilder.CreateIndex(
                name: "idx_usuario_normalized_email",
                table: "usuarios",
                column: "normalized_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuario_normalized_username",
                table: "usuarios",
                column: "normalized_username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuario_persona",
                table: "usuarios",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuario_security_stamp",
                table: "usuarios",
                column: "security_stamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cliente_api_logs");

            migrationBuilder.DropTable(
                name: "dependencia_perfil_config");

            migrationBuilder.DropTable(
                name: "perfil_permiso");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "tokens_confirmacion");

            migrationBuilder.DropTable(
                name: "usuario_perfil_dependencia");

            migrationBuilder.DropTable(
                name: "clientes_api");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "dependencias");

            migrationBuilder.DropTable(
                name: "modos_herencia");

            migrationBuilder.DropTable(
                name: "perfiles");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "personas");
        }
    }
}
