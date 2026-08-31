using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParmeniaHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "convocatorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    requisitos = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    tipo_programa = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    inicio_inscripciones = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    fin_inscripciones = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inicio_programa = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    fin_programa = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    fecha_creacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_convocatorias", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_convocatorias_tipo_programa_estado",
                table: "convocatorias",
                columns: new[] { "tipo_programa", "estado" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "convocatorias");
        }
    }
}
