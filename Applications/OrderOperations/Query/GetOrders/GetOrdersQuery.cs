using AutoMapper;
using MovieStore.DbOperations;

namespace MovieStore.Applications.OrderOperations.Query.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;

        public GetOrdersQuery(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void Handler()
        {
            
        }
    }
}