using System;
using FluentValidation;

namespace MovieStore.Applications.MovieOperations.Command.CreateMovie
{
    public class CreateMovieCommandValidations : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidations()
        {
            RuleFor(command=>command.Model.Price).GreaterThan(0);
            RuleFor(command=>command.Model.PublishDate).LessThan(DateTime.Now);
            RuleForEach(command=>command.Model.Genres).GreaterThan(0);
        }
    }
}