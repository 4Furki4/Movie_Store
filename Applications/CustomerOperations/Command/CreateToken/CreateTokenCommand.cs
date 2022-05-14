using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MovieStore.DbOperations;
using MovieStore.TokenOperations;
using MovieStore.TokenOperations.Models;

namespace MovieStore.Applications.CustomerOperations.Command.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        readonly IConfiguration configuration;
        public  CreateTokenModel Model { get; set; }
        public CreateTokenCommand(MovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        public Token Handler()
        {
            var customer= context.Customers.SingleOrDefault(c=>c.E_Mail ==Model.E_Mail && c.Password == Model.Password);
            if(customer is null)
                throw new InvalidOperationException("E-posta veya şifre hatalı!");
            TokenHandler tokenHandler= new(configuration);
            Token token = tokenHandler.CreateAccessToken(customer);
            customer.RefresToken = token.RefreshToken;
            customer.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(10);
            context.SaveChanges();
            return token;
        }
    }
    public class CreateTokenModel
    {
        public string E_Mail { get; set; }
        public string Password { get; set; }
    }
}