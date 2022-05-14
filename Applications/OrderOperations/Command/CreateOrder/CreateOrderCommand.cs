using AutoMapper;
using MovieStore.DbOperations;

namespace MovieStore.Applications.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommand
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;

        public CreateOrderCommand(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public void Handler()
        {
            
        }
    }
}