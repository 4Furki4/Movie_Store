using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.DbOperations;
using MovieStore.Entities;
using static System.Net.WebRequestMethods;

namespace MovieStore.Applications.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommand
    {
        private readonly MovieStoreDbContext context;
        public CreateOrderModel Model { get; set; }

        public CreateOrderCommand(MovieStoreDbContext context)
        {
            this.context = context;

        }
        
        public void Handler()
        {
            var customer = context.Customers.SingleOrDefault(c=>c.E_Mail==Model.E_Mail.Trim());
            if(customer is null)
                throw new InvalidOperationException("Sipariş oluşturulacak e-mail bulunamadı.");
            List<Movie> movies = new List<Movie>();
            Model.MovieIds.ForEach(delegate(int Id)
            {
                movies = context.Movies.Where(mov=>mov.MovieId==Id).ToList();
            });
            int totalPice=0;
            movies.ForEach(delegate(Movie movie)
            {
                totalPice+=movie.Price;
            });
            
            Order order = new Order
            {
                CustomerId=customer.Id,
                Movies=movies,
                Price=totalPice
            };
            context.Orders.Add(order);
            context.SaveChanges();
        }
    }
    public class CreateOrderModel
    {
        public string E_Mail { get; set; }

        public List<int> MovieIds { get; set; }

    }
}