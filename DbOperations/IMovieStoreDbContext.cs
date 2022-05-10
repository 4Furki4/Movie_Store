using Microsoft.EntityFrameworkCore;
using MovieStore.Entities;

namespace MovieStore.DbOperations
{
    public interface IMovieStoreDbContext
    {
         DbSet<Movie> Movies {get; set;}
         DbSet<Genre> Genres {get; set;}
         DbSet<Cast> Casts {get; set;}
         DbSet<Director> Directors {get; set;}
         DbSet<Order> Orders {get; set;}
         DbSet<Customer> Customers {get; set;}
         DbSet<MovieCast> MovieCasts {get;set;}
         DbSet<MovieGenre> MovieGenres {get;set;}
         int SaveChanges();
    }
}