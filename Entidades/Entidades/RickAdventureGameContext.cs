using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RickAdventureAPI.Data.Entidades;

public partial class RickAdventureGameContext : DbContext
{
    public RickAdventureGameContext()
    {
    }

    public RickAdventureGameContext(DbContextOptions<RickAdventureGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Jugador> Jugadores { get; set; }

    public virtual DbSet<Nivel> Niveles { get; set; }

    public virtual DbSet<Partida> Partidas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Solo configurar si no se pasaron opciones (para testing)
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS2022;Database=RickAdventureGame;Trusted_Connection=True;TrustServerCertificate=true");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.IdJugador).HasName("PK__Jugadore__99E32016EB701D70");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NivelMaximoAlcanzado).HasDefaultValue(1);
            entity.Property(e => e.NombreJugador).HasMaxLength(50);
            entity.Property(e => e.PuntajeTotal).HasDefaultValue(0);
        });

        modelBuilder.Entity<Nivel>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("PK__Niveles__A7F93DEC3019C858");

            entity.Property(e => e.IdNivel).ValueGeneratedNever();
            entity.Property(e => e.Dificultad).HasMaxLength(20);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Partida>(entity =>
        {
            entity.HasKey(e => e.IdPartida).HasName("PK__Partidas__6ED660C7FC4520F8");

            entity.Property(e => e.Completado).HasDefaultValue(false);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.Partida)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partidas__IdJuga__4316F928");

            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.Partida)
                .HasForeignKey(d => d.IdNivel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partidas__IdNive__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
