using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;

namespace MovieStore.Applications.MovieOperations.Query.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        public int MovieId { get; set; }

        public GetMovieDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public MovieDetailViewModel Handler()
        {
            var movie= context.Movies
            .Include(x=>x.Director)
            .Include(x=>x.MovieCasts).ThenInclude(x=>x.Cast)
            .Include(x=>x.MovieGenres).ThenInclude(x=>x.Genre).SingleOrDefault(x=>x.MovieId==MovieId);
            if(movie is null)
                throw new InvalidOperationException("Aradğınız kitap bulunamadı.");
            var result= mapper.Map<MovieDetailViewModel>(movie);
            return result;
        }
    }

    public class MovieDetailViewModel
    {
        public string MovieName { get; set; }
        public DateTime PublishDate { get; set; }
        public int Price { get; set; }
        public string Director { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Casts { get; set; }
    }
}