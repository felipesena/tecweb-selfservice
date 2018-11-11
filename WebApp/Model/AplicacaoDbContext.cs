using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Model
{
    public class AplicacaoDbContext : DbContext
    {
        public AplicacaoDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public AplicacaoDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Setting.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comida>().ToTable("Comidas");
            modelBuilder.Entity<Comida>().Property(i => i.ComidaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Comida>().HasKey(i => i.ComidaId);
            modelBuilder.Entity<Comida>().HasOne(i => i.Categoria).WithMany(c => c.Comidas);

            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Categoria>().Property(i => i.CategoriaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Categoria>().HasKey(i => i.CategoriaId);
            modelBuilder.Entity<Categoria>().HasMany(i => i.Comidas).WithOne(c => c.Categoria);

            modelBuilder.Entity<Usuario>().Property(i => i.UsuarioId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuario>().HasKey(i => i.UsuarioId);
        }

        public DbSet<Comida> Comidas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
