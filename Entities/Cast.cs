using System.Collections.Generic;

namespace MovieStore.Entities
{
    public class Cast
    {
        public int CastId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<MovieCast> MovieCast{get;set;}
    }
}