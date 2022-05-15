using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public List<OrderViewModel> Handler()
        {
            var order = context.Orders.Include(or=>or.Customer).Include(or=>or.Movies).ToList();
            var result= mapper.Map<List<OrderViewModel>>(order);
            return result;
        }
    }
    public class OrderViewModel
    {
        public string Customer { get; set; }
        public List<string> MovieNames { get; set; }
        public int Price { get; set; }

    }
}