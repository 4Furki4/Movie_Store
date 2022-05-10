using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Applications.MovieOperations.Query.GetMovie;
using MovieStore.Applications.MovieOperations.Query.GetMovieDetail;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;

        public MovieController(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMovieQuery query  = new(context, mapper);
            var result= query.Handler();
            return Ok(result);
        }
        [HttpGet("id")]
        public IActionResult GetMovieById(int id)
        {
            GetMovieDetailQuery query = new(context,mapper);
            query.MovieId=id;
            GetMovieDetailQueryValidation validation= new();
            validation.ValidateAndThrow(query);
            var result= query.Handler();
            return Ok(result);
        }
    }
}