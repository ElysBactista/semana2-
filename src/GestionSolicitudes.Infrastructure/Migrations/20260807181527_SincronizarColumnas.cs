using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionSolicitudes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SincronizarColumnas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "Solicitudes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaRegistro" },
                values: new object[] { new DateTime(2026, 8, 7, 18, 15, 26, 738, DateTimeKind.Utc).AddTicks(3959), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaRegistro" },
                values: new object[] { new DateTime(2026, 8, 7, 18, 15, 26, 738, DateTimeKind.Utc).AddTicks(3962), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaRegistro" },
                values: new object[] { new DateTime(2026, 8, 7, 18, 15, 26, 738, DateTimeKind.Utc).AddTicks(3963), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaRegistro" },
                values: new object[] { new DateTime(2026, 8, 7, 18, 15, 26, 738, DateTimeKind.Utc).AddTicks(3964), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaRegistro" },
                values: new object[] { new DateTime(2026, 8, 7, 18, 15, 26, 738, DateTimeKind.Utc).AddTicks(3965), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "Solicitudes");

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7246));

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7250));

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7251));

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "Solicitudes",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2026, 7, 28, 15, 33, 41, 455, DateTimeKind.Utc).AddTicks(7254));
        }
    }
}
