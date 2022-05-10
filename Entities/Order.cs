using System.Collections.Generic;

namespace MovieStore.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<Movie> Movie { get; set; }
        public int Price { get; set; }
    }
}