using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionSolicitudes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRolesYUsuariosSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Insertar solo el nuevo Rol de Residente
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: ["Id", "ConcurrencyStamp", "Name", "NormalizedName"],
                values: ["b1c2d3e4-2222-3333-4444-555566667777", null, "Residente", "RESIDENTE"]);

            // 2. Insertar el nuevo Usuario de Residente
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: ["Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"],
                values: ["a1b2c3d4-2222-3333-4444-555566667777", 0, "e93231a7-4721-4076-bd95-c27f52d0b588", "residente@correo.com", true, false, null, "RESIDENTE@CORREO.COM", "RESIDENTE@CORREO.COM", "AQAAAAIAAYagAAAAEL54tulTVoL/6NyH7Tp1Hjgfs2ymlP9M68jrO/5ALnhWm7DhpBkboMOdlwihir/LQg==", null, false, "3e7d1617-5d6e-4c7f-8c55-7494c8ada077", false, "residente@correo.com"]);

            // 3. Vincular el Usuario Residente con el Rol Residente
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: ["RoleId", "UserId"],
                values: ["b1c2d3e4-2222-3333-4444-555566667777", "a1b2c3d4-2222-3333-4444-555566667777"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: ["RoleId", "UserId"],
                keyValues: ["b1c2d3e4-2222-3333-4444-555566667777", "a1b2c3d4-2222-3333-4444-555566667777"]);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1c2d3e4-2222-3333-4444-555566667777");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-2222-3333-4444-555566667777");
        }
    }
}