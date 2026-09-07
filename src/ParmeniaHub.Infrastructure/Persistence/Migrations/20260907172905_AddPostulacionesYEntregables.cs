using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParmeniaHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPostulacionesYEntregables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "postulaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_proyecto = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    nombre_postulante = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    correo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    etapa_deseada = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    observaciones = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    fecha_creacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postulaciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "entregables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    postulacion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    comentarios = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    fecha_creacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entregables", x => x.id);
                    table.ForeignKey(
                        name: "FK_entregables_postulaciones_postulacion_id",
                        column: x => x.postulacion_id,
                        principalTable: "postulaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entregables_postulacion_id",
                table: "entregables",
                column: "postulacion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entregables");

            migrationBuilder.DropTable(
                name: "postulaciones");
        }
    }
}
