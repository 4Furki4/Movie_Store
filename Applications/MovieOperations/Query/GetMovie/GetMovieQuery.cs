using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Applications.MovieOperations.Query.GetMovie
{
    public class GetMovieQuery
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        public GetMovieQuery(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public List<MovieModel> Handler()
        {
            // var movie = context.Movies.Include(x=>x.MovieGenres).Include(y=>y.MovieCasts).OrderBy(x=>x.MovieId).ToList<Movie>()
            var movie = context.Movies
            .Include(x=>x.Director)
            .Include(x=>x.MovieCasts).ThenInclude(X=>X.Cast)
            .Include(x=>x.MovieGenres).ThenInclude(x=>x.Genre)
            .OrderBy(x=>x.MovieId).ToList();
            var result = mapper.Map<List<MovieModel>>(movie);
            return result;
        }
    }

    public class MovieModel
    {
        public string MovieName { get; set; }
        public DateTime PublishDate { get; set; }
        public int Price { get; set; }
        public string Director { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Casts { get; set; }

    }
}