using FluentValidation;

namespace MovieStore.Applications.MovieOperations.Command.DeleteMovie
{
    public class DeleteMovieCommandValidations : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidations()
        {
            RuleFor(command=>command.MovieId).GreaterThan(0);
        }
    }
}