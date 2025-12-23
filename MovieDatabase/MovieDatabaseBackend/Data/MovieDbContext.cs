using MovieDatabaseBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseBackend.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> Persons { get; set; }

        public string DbPath { get; }

        public MovieDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "Movie.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Directors)
                .WithMany(p => p.DirectedMovies);
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.LeadActors)
                .WithMany(p => p.ActedInMovies);
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Writer)
                .WithMany(p => p.WrittenMovies);
        }
    }
}
