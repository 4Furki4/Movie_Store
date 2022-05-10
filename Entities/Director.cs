using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DirectorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Movie> Movie {get;set;}
    }
}