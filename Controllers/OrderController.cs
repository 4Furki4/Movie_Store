using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Applications.OrderOperations.Command.CreateOrder;
using MovieStore.Applications.OrderOperations.Query.GetOrders;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        public OrderController(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
        [HttpGet]
        public ActionResult ListOrders()
        {
            GetOrdersQuery query = new(context,mapper);
            var result = query.Handler();
            return Ok(result);

        }
    }
}