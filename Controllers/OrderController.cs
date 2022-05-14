using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Applications.OrderOperations.Command.CreateOrder;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly MovieStoreDbContext context;
        public OrderController(MovieStoreDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderModel orderModel)
        {
            CreateOrderCommand command = new(context);
            CreateOrderCommandValidations validations = new();
            command.Model=orderModel;
            validations.Validate(command,opt=>opt.ThrowOnFailures());
            command.Handler();
            return Ok();
        }
    }
}