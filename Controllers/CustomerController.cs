using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Applications.CustomerOperations.Command.CreateCustomer;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        public CustomerController(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public void CreateCustomer([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new(context,mapper);
            CreateCustomerCommandValidations validations = new();
            command.Model= newCustomer;
            validations.ValidateAndThrow(command);
            command.Handler();
        }
    }
}