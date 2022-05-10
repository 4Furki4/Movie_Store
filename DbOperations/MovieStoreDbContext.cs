using Microsoft.EntityFrameworkCore;
using MovieStore.Entities;

namespace MovieStore.DbOperations
{
    public class MovieStoreDbContext : DbContext,IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {   }
        public DbSet<Movie> Movies {get; set;}
        public DbSet<Genre> Genres {get; set;}
        public DbSet<Cast> Casts {get; set;}
        public DbSet<Director> Directors {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<Customer> Customers {get; set;}
        public DbSet<MovieCast> MovieCasts {get;set;}
        public DbSet<MovieGenre> MovieGenres {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasOne(x=>x.Movie).WithMany(mg=>mg.MovieGenres).HasForeignKey(mi=>mi.MovieId);
            modelBuilder.Entity<MovieCast>().HasOne(x=>x.Movie).WithMany(mg=>mg.MovieCasts).HasForeignKey(mi=>mi.MovieId);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        
    }
}