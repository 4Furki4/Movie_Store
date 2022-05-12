using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStore.Entities;

namespace MovieStore.DbOperations
{
    public class DataGenerator
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if(context.Movies.Any())
                    return;
                context.Genres.AddRange
                (
                    new Genre{Name="Action"},
                    new Genre{Name="Adventure"},
                    new Genre{Name="Comedy"},
                    new Genre{Name="Drama"},
                    new Genre{Name="Horror"},
                    new Genre{Name="Thriller"}
                );
                context.Casts.AddRange
                (
                    new Cast{Name="Viggo",Surname="Vortensen"}, //green book
                    new Cast{Name="Mahershala",Surname="Ali"}, //green book
                    new Cast{Name="Robert", Surname="Pattison"}, //the batman
                    new Cast{Name="Zoë", Surname="Kravitz"} // the batman

                );
                context.Directors.AddRange
                (
                    new Director {Name="Petter", Surname="Farrely"},
                    new Director {Name ="Matt", Surname= "Reeves"}
                );
                context.Movies.AddRange
                (// 
                    new Movie
                    {
                        MovieName="Green Book",
                        PublishDate= new DateTime(2018,09,11),
                        DirectorId=1,
                        Price=1000
                    },
                    new Movie
                    {
                        MovieName="The Batman",
                        PublishDate = new DateTime (2022, 03,01),
                        DirectorId=2,
                        Price=1000
                    }

                );
                context.MovieCasts.AddRange(movieCasts);
                context.MovieGenres.AddRange(movieGenres);
                context.SaveChanges();
            }
        }
        private static List<MovieCast> movieCasts = new List<MovieCast>
        {
            new MovieCast(){MovieId=1, CastId=1},
            new MovieCast(){MovieId=1,CastId=2},
            new MovieCast(){MovieId=2,CastId=3},
            new MovieCast(){MovieId=2,CastId=4}
        };
        private static List<MovieGenre> movieGenres = new List<MovieGenre>
        {
            new MovieGenre(){GenreId=4, MovieId=1},
            new MovieGenre(){GenreId=3, MovieId=1},
            new MovieGenre(){GenreId=1, MovieId=2},
            new MovieGenre(){GenreId=4, MovieId=2}
        };
    }
}