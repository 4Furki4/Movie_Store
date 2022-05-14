using FluentValidation;

namespace MovieStore.Applications.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommandValidations : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidations()
        {
            RuleFor(command=>command.CustomerE_Mail).MinimumLength(3).EmailAddress(mode:FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }
    }
}