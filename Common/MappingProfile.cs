using System.Linq;
using AutoMapper;
using MovieStore.Applications.CastOperations.Command.CreateCast;
using MovieStore.Applications.CustomerOperations.Command.CreateCustomer;
using MovieStore.Applications.DirectorOperations.Command.CreateDirector;
using MovieStore.Applications.GenreOperations.Command.CreateGenre;
using MovieStore.Applications.MovieOperations.Command.CreateMovie;
using MovieStore.Applications.MovieOperations.Query.GetMovie;
using MovieStore.Applications.MovieOperations.Query.GetMovieDetail;
using MovieStore.Applications.OrderOperations.Query.GetOrders;
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
            .ForMember(dest=>dest.Genres,opt=>opt.MapFrom(src=>src.MovieGenres.Select(x=>x.Genre.Name)));
            
            //GetMovieDetail Mapping
            CreateMap<Movie, MovieDetailViewModel>().
            ForMember(dest=>dest.Director,opt=>opt.MapFrom(src=>src.Director.Name+" "+ src.Director.Surname))
            .ForMember(dest=>dest.Casts,opt=>opt.MapFrom(src=>src.MovieCasts.Select(x=>x.Cast.Name+" "+x.Cast.Surname)))
            .ForMember(dest=>dest.Genres,opt=>opt.MapFrom(src=>src.MovieGenres.Select(x=>x.Genre.Name)));

            //MovieCast ve MovieGenre ortak sınıflarının maplenmesi
            CreateMap<MovieCastModel,MovieCast>();
            CreateMap<MovieGenreModel,MovieGenre>();

            CreateMap<CreateDirectorModel,Director>();

            CreateMap<CreateMovieModel,Movie>();

            //Yeni bir müşteri oluştururken olası gereksiz boşlukları(whitespaceleri) kaldırıp mapliyoruz.
            CreateMap<CreateCustomerModel,Customer>()
            .ForMember(dest=>dest.E_Mail,opt=>opt.MapFrom(src=>src.E_Mail.Trim())) 
            .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name.Trim()))
            .ForMember(dest=>dest.Surname,opt=>opt.MapFrom(src=>src.Surname.Trim()))
            .ForMember(dest=>dest.Password,opt=>opt.MapFrom(src=>src.Password.Trim())); 
            
            CreateMap<Order,OrderViewModel>()
            .ForMember(dest=>dest.Customer,opt=>opt.MapFrom(src=>src.Customer.Name +" "+src.Customer.Surname))
            .ForMember(dest=>dest.MovieNames,opt=>opt.MapFrom(src=>src.Movies.Select(mv=>mv.MovieName)));
        }
    }
}