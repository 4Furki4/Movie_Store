using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.Applications.CastOperations.Command.CreateCast;
using MovieStore.Applications.DirectorOperations.Command.CreateDirector;
using MovieStore.Applications.GenreOperations.Command.CreateGenre;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Applications.MovieOperations.Command.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model;
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;

        public CreateMovieCommand(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Handler()
        {
            var movie = context.Movies.SingleOrDefault(x=>x.MovieName==Model.MovieName && x.Director.Name== Model.Director.Name);
            if (movie is not null)
                throw new InvalidOperationException("Eklemek istediğiniz kitap zaten mevcut!");
            var Director= mapper.Map<Director>(Model.Director); //Yönetmenin ismi ve soyimi maplendi.
            Director.Movie = new List<Movie>(); //Yönetmenin filmi için nesne oluşturuldu lakin tüm atamalar yapıldıktan sonra film atanacak.
            var castNames= Model.Casts.Select(s=>s.Name).ToList(); //Oyuncular liste olarak alındı ve bunlar select ile isimlerine göres eçildi bir listeye atandı.
            var castSurnames= Model.Casts.Select(s=>s.Surname).ToList(); //Burada da soyismine göre.
            List<Cast> casts= new List<Cast>(); //oyuncuları atayacağım liste.
            foreach (var item in castNames)
            {
                foreach (var name in item)
                    casts.Add(new Cast{Name=name}); //yeni oyuncu nesneleri oluşturuyorum, isimlerini veriyorum ve bunları listeme ekliyorum.
            }
            foreach (var item in castSurnames) 
            {
                foreach (var Surname in item) //burada da soyisimlerini veriyorum.
                    foreach (var cast in casts)
                        cast.Surname=Surname;
            }
            context.Casts.AddRange(casts); // oyuncuları databasedeki Casts'e ekliyorum 
            movie=mapper.Map<Movie>(Model);
            context.Movies.Add(movie);
            context.SaveChanges();
            List<MovieCastModel> movieCastModels=new List<MovieCastModel>();
            List<MovieGenreModel> movieGenreModels=new List<MovieGenreModel>();
            //MovieCast ve MovieGenre için Id atamalarını yapıyorum.
            foreach (var cast in casts) 
            {
                MovieCastModel MC_Model= new();
                MC_Model.CastId=cast.CastId;
                MC_Model.MovieId=movie.MovieId;
                movieCastModels.Add(MC_Model);
            }
            var mc= mapper.Map<List<MovieCast>>(movieCastModels); //DBye atanacak MovieCast maplendi.
            context.MovieCasts.AddRange(mc);
            foreach (var genreId in Model.Genres)
            {
                MovieGenreModel MG_Model= new();
                MG_Model.GenreId=genreId;
                MG_Model.MovieId=movie.MovieId;
                movieGenreModels.Add(MG_Model);
            }
            var mg= mapper.Map<List<MovieGenre>>(movieGenreModels); //DBye atanacak MovieGenre maplendi.
            context.MovieGenres.AddRange(mg);
            Director.Movie.Add(movie); //Yönetmenin filmi, filmin atamaları yapıldıktan sonra eklendi ve db değişiklikleri kaydedildi.
            context.SaveChanges();

        }

    }
    public class CreateMovieModel
    {
        public string MovieName { get; set; }
        public DateTime PublishDate { get; set; }
        public int Price { get; set; }
        public CreateDirectorModel Director { get; set; }
        public List<int> Genres { get; set; }
        public List<CreateCastModel> Casts { get; set; }
    }
    public class MovieCastModel
    {
        public int MovieId { get; set; }
        public int CastId { get; set; }

    }
    public class MovieGenreModel
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}