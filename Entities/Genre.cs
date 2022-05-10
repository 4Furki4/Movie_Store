using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        public string Name { get; set; }

    }
}