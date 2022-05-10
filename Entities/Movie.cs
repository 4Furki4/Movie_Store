using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public DateTime PublishDate { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; } //Bir filmin tek yönetmeni olduğunu kabul ediyorum.
        public List<MovieCast> MovieCasts { get; set; } //Bir filmin birden fazla oyunucusu vardır.
        public List<MovieGenre> MovieGenres { get; set; } //Bir filmin birden fazla türü olabilir.
        public int Price { get; set; }

    }
}