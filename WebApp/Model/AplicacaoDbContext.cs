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
            modelBuilder.Entity<Comida>().HasMany(i => i.Pratos).WithOne(c => c.Comida);

            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Categoria>().Property(i => i.CategoriaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Categoria>().HasKey(i => i.CategoriaId);
            modelBuilder.Entity<Categoria>().HasMany(i => i.Comidas).WithOne(c => c.Categoria);
            modelBuilder.Entity<Categoria>().HasMany(i => i.Pratos).WithOne(c => c.Categoria);

            modelBuilder.Entity<Usuario>().Property(i => i.UsuarioId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuario>().HasKey(i => i.UsuarioId);
            modelBuilder.Entity<Usuario>().HasMany(i => i.Pratos).WithOne(u => u.Usuario);
            modelBuilder.Entity<Usuario>().HasMany(i => i.Consumos).WithOne(u => u.Usuario);

            modelBuilder.Entity<Prato>().Property(i => i.PratoId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Prato>().HasKey(i => i.PratoId);
            modelBuilder.Entity<Prato>().HasMany(i => i.Alimentos).WithOne(p => p.Prato);
            modelBuilder.Entity<Prato>().HasOne(i => i.Usuario).WithMany(u => u.Pratos);

            modelBuilder.Entity<AlimentoPrato>().Property(i => i.AlimentoPratoId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AlimentoPrato>().HasKey(i => i.AlimentoPratoId);
            modelBuilder.Entity<AlimentoPrato>().HasOne(i => i.Comida).WithMany(p => p.Pratos);
            modelBuilder.Entity<AlimentoPrato>().HasOne(i => i.Prato).WithMany(a => a.Alimentos);

            modelBuilder.Entity<HistoricoConsumo>().Property(i => i.HistoricoConsumoId).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistoricoConsumo>().HasKey(i => i.HistoricoConsumoId);
            modelBuilder.Entity<HistoricoConsumo>().HasOne(u => u.Usuario).WithMany(c => c.Consumos);
            modelBuilder.Entity<HistoricoConsumo>().HasOne(p => p.Prato);
        }

        public DbSet<Comida> Comidas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prato> Pratos { get; set; }
        public DbSet<AlimentoPrato> AlimentoPratos { get; set; }
        public DbSet<HistoricoConsumo> HistoricoConsumos { get; set; }
    }
}
