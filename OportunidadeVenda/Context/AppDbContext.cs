using Microsoft.EntityFrameworkCore;
using OportunidadeVenda.Data;

namespace OportunidadeVenda.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Oportunidade>? Oportunidade { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Name)
                .HasMaxLength(80).IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(p => p.Email)
                .HasMaxLength(80).IsRequired();

            modelBuilder.Entity<Usuario>()
               .Property(p => p.Regiao)
               .HasMaxLength(80).IsRequired();

            modelBuilder.Entity<Oportunidade>()
               .Property(p => p.Cnpj)
               .HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Oportunidade>()
               .Property(p => p.Name)
               .HasMaxLength(80).IsRequired();

            modelBuilder.Entity<Oportunidade>()
               .Property(p => p.Valor)
               .HasPrecision(10,2).IsRequired();

            modelBuilder.Entity<Oportunidade>()
                .HasOne(p => p.Usuario)
                .WithMany(p => p.Oportunidades)
                .HasForeignKey(p => p.IdUsuario);
        }
        
    }
}
