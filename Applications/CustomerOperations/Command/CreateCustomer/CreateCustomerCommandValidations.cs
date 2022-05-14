using FluentValidation;

namespace MovieStore.Applications.CustomerOperations.Command.CreateCustomer
{
    public class CreateCustomerCommandValidations : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidations()
        {
            RuleFor(command=>command.Model.E_Mail).MinimumLength(1).EmailAddress(mode:FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(command=>command.Model.Name).MinimumLength(1);
            RuleFor(command=>command.Model.Surname).MinimumLength(1);
            RuleFor(command=>command.Model.Password).MinimumLength(4);
        }
    }
}