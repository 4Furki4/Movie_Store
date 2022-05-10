using FluentValidation;

namespace MovieStore.Applications.MovieOperations.Query.GetMovieDetail
{
    public class GetMovieDetailQueryValidation : AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidation()
        {
            RuleFor(query=>query.MovieId).GreaterThan(0);
        }
    }
}