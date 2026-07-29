using GestionSolicitudes.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GestionSolicitudes.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Solicitud> Solicitudes => Set<Solicitud>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Solicitud>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Solicitante).IsRequired();
        });

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