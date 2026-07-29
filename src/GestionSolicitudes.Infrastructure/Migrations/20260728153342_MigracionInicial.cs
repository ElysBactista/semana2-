using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
namespace GestionSolicitudes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solicitante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Solicitudes",
                columns: ["Id", "Activo", "Descripcion", "Estado", "FechaActualizacion", "FechaCreacion", "Solicitante", "Titulo"],
                values: new object[,]
                {
                    { 1, true, "", 1, null, new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7246), "Elys Camila Batista Encarnacion ", "Instalación de equipo" },
                    { 2, true, "", 1, null, new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7250), "Pedro Martinez Gonzales ", "Mantenimiento de software" },
                    { 3, true, "", 1, null, new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7251), "Leonel Leon Pica piedra  ", "Actualización de sistema" },
                    { 4, true, "", 1, null, new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7253), "Giana pichardo Chiguaua ", " Revision de Red " },
                    { 5, true, "", 1, null, new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7254), " Rossi Mosqueta Garcia", " Actualizacion de licenias " }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitudes");
        }
    }
}
