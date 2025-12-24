using MovieDatabaseBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseBackend.Data
{
    public class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> Persons { get; set; }

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
