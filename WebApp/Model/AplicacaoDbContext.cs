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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().Property(i => i.UsuarioId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Comida>().ToTable("Comidas");
            modelBuilder.Entity<Comida>().Property(i => i.ComidaId).ValueGeneratedOnAdd();            

            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Categoria>().Property(i => i.CategoriaId).ValueGeneratedOnAdd();            
        }
    }
}
