using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Applications.MovieOperations.Command.UpdateMovie
{
    
    public class UpdateMovieCommand
    {   // AutoMapper kullanmadım çünkü  put handlerındaki değişkenlerin atamasını elle yazmak daha güvenli.
        public UpdateMovieModel Model{get; set;}
        private readonly MovieStoreDbContext context;
        public int MovieId { get; set; }

        public UpdateMovieCommand(MovieStoreDbContext context)
        {
            this.context = context;
        }

        public void Handler()
        {
            var movie = context.Movies.Include(x=>x.Director)
            .Include(x=>x.MovieCasts).ThenInclude(x=>x.Cast)
            .Include(x=>x.MovieGenres).ThenInclude(x=>x.Genre).SingleOrDefault(x=>x.MovieId==MovieId);
            if(movie is null)
                throw new InvalidOperationException("Değiştirmek istediğiniz kitap bulunamadı.");
            movie.MovieName = Model.MovieName == string.Empty || Model.MovieName == "string" ? movie.MovieName: Model.MovieName;
            movie.PublishDate = Model.PublishDate == default ? movie.PublishDate : Model.PublishDate;
            movie.Price = Model.Price == default ? movie.Price : Model.Price;
            movie.Director.Name= Model.DirectorName == string.Empty || Model.DirectorName == "string" ? movie.Director.Name : Model.DirectorName;
            movie.Director.Surname= Model.DirectorSurname == string.Empty || Model.DirectorSurname == "string" ? movie.Director.Surname : Model.DirectorSurname;
            var MG= context.MovieGenres.Where(mg=>mg.MovieId==MovieId).ToList();
            context.MovieGenres.RemoveRange(MG); // Key sayısı çakışmaması için önce databaseden MovieGenre'mızı siliyoruz ama önceden MG'ye atadığımız için kullanabileceğiz.
            context.SaveChanges();
            if (MG.Count>Model.GenreIds.Count) //Güncel genre sayısı, girilenden fazlaysa
            {
                int theDifference= MG.Count-Model.GenreIds.Count; 
                for (int i = 0; i < Model.GenreIds.Count; i++) //Gelen yeni genrelar, güncel genreların yerini alacak
                    MG[i].GenreId=Model.GenreIds[i];
                for (int i = Model.GenreIds.Count; i < theDifference; i++)
                    MG.RemoveAt(i); // artıklar silinecek.
            }
            else if(MG.Count<Model.GenreIds.Count) //Güncel genre sayısı, girilenden azsa
            {
                int theDifference= Model.GenreIds.Count-MG.Count;
                for (int i = 0; i < Model.GenreIds.Count-1; i++)
                    MG[i].GenreId=Model.GenreIds[i];
                for (int i = 0; i < theDifference; i++)
                {
                    MG.Add(new MovieGenre{GenreId=Model.GenreIds[i],MovieId=MovieId}); // Az olduğu için gelen yenileri de eklemeliyiz.
                }
            }
            else
            {
                for (int i = 0; i < MG.Count; i++) //güncel ile gelen aynıysa
                    MG[i].GenreId=Model.GenreIds[i];
            }
            context.MovieGenres.AddRange(MG); // yeni oluşturduğumuz MovieGenre GenreId ve MovieId listesini dbye atıp kaydediyoruz.
            context.SaveChanges();
        }
    }
    public class UpdateMovieModel
    {
        public string MovieName { get; set; }
        public DateTime PublishDate { get; set; }
        public string DirectorName { get; set; }
        public string DirectorSurname {get; set;}
        public int Price { get; set; }
        public List<int> GenreIds { get; set; }

    }
}