using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParmeniaHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHistorialDeEstados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "historial_postulaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    postulacion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    observaciones = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    fecha = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historial_postulaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_historial_postulaciones_postulaciones_postulacion_id",
                        column: x => x.postulacion_id,
                        principalTable: "postulaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "revisiones_entregables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entregable_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    comentarios = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    fecha = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_revisiones_entregables", x => x.id);
                    table.ForeignKey(
                        name: "FK_revisiones_entregables_entregables_entregable_id",
                        column: x => x.entregable_id,
                        principalTable: "entregables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_historial_postulaciones_postulacion_id",
                table: "historial_postulaciones",
                column: "postulacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_revisiones_entregables_entregable_id",
                table: "revisiones_entregables",
                column: "entregable_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historial_postulaciones");

            migrationBuilder.DropTable(
                name: "revisiones_entregables");
        }
    }
}
