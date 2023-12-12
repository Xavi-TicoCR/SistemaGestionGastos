using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SistemaGestionGastos.Models
{
    public partial class SistemaGestionGastosContext : DbContext
    {
        public SistemaGestionGastosContext()
        {
        }

        public SistemaGestionGastosContext(DbContextOptions<SistemaGestionGastosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administradore> Administradores { get; set; } = null!;
        public virtual DbSet<CategoriasGasto> CategoriasGastos { get; set; } = null!;
        public virtual DbSet<Gasto> Gastos { get; set; } = null!;
        public virtual DbSet<Informe> Informes { get; set; } = null!;
        public virtual DbSet<LimitesGasto> LimitesGastos { get; set; } = null!;
        public virtual DbSet<Notificacione> Notificaciones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administradore>(entity =>
            {
                entity.HasKey(e => e.IdAdministrador)
                    .HasName("PK__Administ__2D89616F299D00BF");

                entity.Property(e => e.IdAdministrador).HasColumnName("ID_Administrador");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Rol).HasMaxLength(255);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Administradores)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Administr__ID_Us__46E78A0C");
            });

            modelBuilder.Entity<CategoriasGasto>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__02AA0785BCC13DA2");

                entity.ToTable("Categorias_Gastos");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.NombreCategoria)
                    .HasMaxLength(255)
                    .HasColumnName("Nombre_Categoria");
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.HasKey(e => e.IdGasto)
                    .HasName("PK__Gastos__57709B7A166F2158");

                entity.Property(e => e.IdGasto).HasColumnName("ID_Gasto");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Gastos__ID_Categ__3D5E1FD2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Gastos__ID_Usuar__3C69FB99");
            });

            modelBuilder.Entity<Informe>(entity =>
            {
                entity.HasKey(e => e.IdInforme)
                    .HasName("PK__Informes__91D0EA9C571AF085");

                entity.Property(e => e.IdInforme).HasColumnName("ID_Informe");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Periodo).HasMaxLength(255);

                entity.Property(e => e.TotalGastos)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Total_Gastos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Informes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Informes__ID_Usu__440B1D61");
            });

            modelBuilder.Entity<LimitesGasto>(entity =>
            {
                entity.HasKey(e => e.IdLimite)
                    .HasName("PK__Limites___F8AF019AB5C63EF2");

                entity.ToTable("Limites_Gastos");

                entity.Property(e => e.IdLimite).HasColumnName("ID_Limite");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.MontoMaximo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Monto_Maximo");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.LimitesGastos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Limites_G__ID_Ca__412EB0B6");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.LimitesGastos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Limites_G__ID_Us__403A8C7D");
            });

            modelBuilder.Entity<Notificacione>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion)
                    .HasName("PK__Notifica__283380F1E4E1905D");

                entity.Property(e => e.IdNotificacion).HasColumnName("ID_Notificacion");

                entity.Property(e => e.Estado).HasMaxLength(255);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Notificac__ID_Us__49C3F6B7");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__DE4431C528007816");

                entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__85947816746D95E4")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Contraseña).HasMaxLength(255);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(255)
                    .HasColumnName("Correo_Electronico");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
