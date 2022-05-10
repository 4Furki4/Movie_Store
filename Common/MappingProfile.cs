using System.Linq;
using AutoMapper;
using MovieStore.Applications.MovieOperations.Query.GetMovie;
using MovieStore.Applications.MovieOperations.Query.GetMovieDetail;
using MovieStore.Entities;

namespace MovieStore.Common
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie,MovieModel>()
            .ForMember(dest=>dest.Director,opt=>opt.MapFrom(src=>src.Director.Name+" "+ src.Director.Surname)) //MovieModel'de Yönetmen Movie nesnemizden yönlendirildi.
            .ForMember(dest=>dest.Casts,opt=>opt.MapFrom(src=>src.MovieCasts.Select(x=>x.Cast.Name +" "+ x.Cast.Surname))) // Include ile dahil ettiğimiz MovieCastten Oyuncunun ismi ve soyismi yönlendirildi.
            .ForMember(dest=>dest.Genres,opt=>opt.MapFrom(src=>src.MovieGenres.Select(x=>x.Genre.Name))); // Yukardakinin aynısı.
            CreateMap<Movie, MovieDetailViewModel>().
            ForMember(dest=>dest.Director,opt=>opt.MapFrom(src=>src.Director.Name+" "+ src.Director.Surname))
            .ForMember(dest=>dest.Casts,opt=>opt.MapFrom(src=>src.MovieCasts.Select(x=>x.Cast.Name+" "+x.Cast.Surname)))
            .ForMember(dest=>dest.Genres,opt=>opt.MapFrom(src=>src.MovieGenres.Select(x=>x.Genre.Name+" "+x.Genre.Name)));
        }
    }
}