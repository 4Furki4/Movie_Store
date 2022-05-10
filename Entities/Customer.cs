using System.Collections.Generic;

namespace MovieStore.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Order> Order { get; set; }
    }
}