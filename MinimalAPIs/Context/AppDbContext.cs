using Microsoft.EntityFrameworkCore;
using MinimalAPIs.Models;

namespace MinimalAPIs.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game>? Games { get; set; }
        public DbSet<Genero>? Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>()
                .HasKey(c => c.GeneroId);

            modelBuilder.Entity<Genero>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Genero>()
                .Property(c => c.Description)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .HasKey(c => c.GameId);

            modelBuilder.Entity<Game>()
                .Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(c => c.Description)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(c => c.Price)
                .HasPrecision(14,2)
                .IsRequired();

            modelBuilder.Entity<Game>()
               .Property(c => c.Picture)
                .HasMaxLength(500)
               .IsRequired(false);

            modelBuilder.Entity<Game>()
               .Property(c => c.PurchaseDate)
               .IsRequired();

            modelBuilder.Entity<Game>()
               .Property(c => c.Stock)
               .IsRequired();

            modelBuilder.Entity<Game>()
                .HasOne<Genero>(c => c.Genero)
                .WithMany(p => p.Games)
                .HasForeignKey(c => c.GeneroId);
        }
    }
}
