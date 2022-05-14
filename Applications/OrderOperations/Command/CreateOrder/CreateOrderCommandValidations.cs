using FluentValidation;

namespace MovieStore.Applications.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommandValidations : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidations()
        {
            RuleForEach(command=>command.Model.MovieIds).GreaterThan(0);
            RuleFor(command=>command.Model.E_Mail).MinimumLength(4).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }
    }
}