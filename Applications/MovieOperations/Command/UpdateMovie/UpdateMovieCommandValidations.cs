using System;
using FluentValidation;

namespace MovieStore.Applications.MovieOperations.Command.UpdateMovie
{
    public class UpdateMovieCommandValidations : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidations()
        {
            RuleFor(command=>command.MovieId).GreaterThan(0);
            RuleFor(command=>command.Model.DirectorName).MinimumLength(1);
            RuleFor(command=>command.Model.DirectorSurname).MinimumLength(1);
            RuleForEach(command=>command.Model.GenreIds).GreaterThan(0);
            RuleFor(command=>command.Model.PublishDate).LessThan(DateTime.Now);
            RuleFor(command=>command.Model.Price).GreaterThan(0);
            RuleFor(command=>command.Model.MovieName).MinimumLength(1);
        }
    }
}