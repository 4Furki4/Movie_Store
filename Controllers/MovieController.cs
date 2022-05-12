using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Applications.MovieOperations.Command.CreateMovie;
using MovieStore.Applications.MovieOperations.Command.DeleteMovie;
using MovieStore.Applications.MovieOperations.Command.UpdateMovie;
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
        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieModel movieModel)
        {
            CreateMovieCommand command = new(context,mapper);
            command.Model = movieModel;
            CreateMovieCommandValidations validationRules= new();
            validationRules.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
        [HttpPut("id")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieModel updateMovieModel, int id)
        {
            UpdateMovieCommand command = new(context);
            command.MovieId=id;
            command.Model=updateMovieModel;
            UpdateMovieCommandValidations validations=new();
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteMovie(int id)
        {
            DeleteMovieCommand command = new(context);
            DeleteMovieCommandValidations validations = new();
            command.MovieId=id;
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
    }
}