using GestionSolicitudes.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionSolicitudes.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    // DbSet que representa la tabla de Solicitudes en SQL Server
    public DbSet<Solicitud> Solicitudes => Set<Solicitud>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Crucial para las tablas de Identity

        // 1. Configuración de la entidad Solicitud (Restricciones)
        modelBuilder.Entity<Solicitud>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Solicitante).IsRequired();
        });

        // 2. IDs Fijos para Roles y Usuarios en el Seed
        var adminRoleId = "b1c2d3e4-1111-2222-3333-444455556666";
        var residenteRoleId = "b1c2d3e4-2222-3333-4444-555566667777";

        var adminUserId = "a1b2c3d4-1111-2222-3333-444455556666";
        var residenteUserId = "a1b2c3d4-2222-3333-4444-555566667777";

        // 3. Seed de Roles
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole
            {
                Id = residenteRoleId,
                Name = "Residente",
                NormalizedName = "RESIDENTE"
            }
        );

        // 4. Hasher de contraseñas de Identity
        var hasher = new PasswordHasher<IdentityUser>();

        // Usuario Administrador (admin@migracion.gob.do / Admin123*)
        var adminUser = new IdentityUser
        {
            Id = adminUserId,
            UserName = "admin@migracion.gob.do",
            NormalizedUserName = "ADMIN@MIGRACION.GOB.DO",
            Email = "admin@migracion.gob.do",
            NormalizedEmail = "ADMIN@MIGRACION.GOB.DO",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123*");

        // Usuario Residente (residente@correo.com / Residente123*)
        var residenteUser = new IdentityUser
        {
            Id = residenteUserId,
            UserName = "residente@correo.com",
            NormalizedUserName = "RESIDENTE@CORREO.COM",
            Email = "residente@correo.com",
            NormalizedEmail = "RESIDENTE@CORREO.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        residenteUser.PasswordHash = hasher.HashPassword(residenteUser, "Residente123*");

        // Seed de Usuarios
        modelBuilder.Entity<IdentityUser>().HasData(adminUser, residenteUser);

        // 5. Asignación de Roles a Usuarios (IdentityUserRole)
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            },
            new IdentityUserRole<string>
            {
                RoleId = residenteRoleId,
                UserId = residenteUserId
            }
        );

        // 6. Seed de Solicitudes existentes de prueba
        modelBuilder.Entity<Solicitud>().HasData(
            new Solicitud
            {
                Id = 1,
                Titulo = "Instalación de equipo",
                Solicitante = "Elys Camila Batista Encarnacion ",
            },
            new Solicitud
            {
                Id = 2,
                Titulo = "Mantenimiento de software",
                Solicitante = "Pedro Martinez Gonzales ",
            },
            new Solicitud
            {
                Id = 3,
                Titulo = "Actualización de sistema",
                Solicitante = "Leonel Leon Pica piedra  ",
            },
            new Solicitud
            {
                Id = 4,
                Titulo = " Revision de Red ",
                Solicitante = "Giana pichardo Chiguaua ",
            },
            new Solicitud
            {
                Id = 5,
                Titulo = " Actualizacion de licenias ",
                Solicitante = " Rossi Mosqueta Garcia",
            }
        );
    }
}