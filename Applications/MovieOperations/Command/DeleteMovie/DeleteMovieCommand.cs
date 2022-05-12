using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;

namespace MovieStore.Applications.MovieOperations.Command.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly MovieStoreDbContext context;
        public int MovieId { get; set; }
        public DeleteMovieCommand(MovieStoreDbContext context)
        {
            this.context = context;
        }

        public void Handler()
        {
            var movie = context.Movies.Include(x=>x.Director)
            .Include(x=>x.MovieCasts).ThenInclude(x=>x.Cast)
            .Include(x=>x.MovieGenres).ThenInclude(x=>x.Genre).SingleOrDefault(x=>x.MovieId==MovieId);
            if(movie is null)
                throw new InvalidOperationException("Silmek istediğiniz kitap bulunamadı.");
            context.MovieGenres.RemoveRange(movie.MovieGenres);
            context.MovieCasts.RemoveRange(movie.MovieCasts);
            context.Directors.Remove(movie.Director);
            context.Movies.Remove(movie);
            context.SaveChanges();
        }
    }
}