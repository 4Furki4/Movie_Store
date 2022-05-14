using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieStore.Applications.CustomerOperations.Command.CreateCustomer;
using MovieStore.Applications.CustomerOperations.Command.CreateToken;
using MovieStore.Applications.CustomerOperations.Command.DeleteCustomer;
using MovieStore.DbOperations;
using MovieStore.TokenOperations.Models;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        readonly IConfiguration configuration; //appsetting altındaki verilere ulaşmayı sağlar
        public CustomerController(MovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new(context,mapper, configuration);
            CreateCustomerCommandValidations validations = new();
            command.Model= newCustomer;
            validations.ValidateAndThrow(command);
            command.Handler();
            return Ok();
        }
        [HttpDelete("{email}")]
        public IActionResult DeleteCustomer(string email)
        {
            DeleteCustomerCommand command = new(context);
            DeleteCustomerCommandValidations validation= new();
            command.CustomerE_Mail=email.Trim();
            validation.Validate(command,opt=>opt.ThrowOnFailures()); //ValidateAndThrow metodunuz uzun yazılış şekliymiş.
            command.Handler();
            return Ok();
        }
        [HttpPost("conntect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(context,mapper,configuration);
            command.Model=login;
            var token= command.Handler();
            return token;
        }
    }
}