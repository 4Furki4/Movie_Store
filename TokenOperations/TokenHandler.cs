using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieStore.Entities;
using MovieStore.TokenOperations.Models;

namespace MovieStore.TokenOperations
{
    public class TokenHandler
    {
        private IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Token CreateAccessToken(Customer customer) //kullanıcıya özel token oluşturulacak.
        {
            Token tokenModel = new();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new(key,SecurityAlgorithms.HmacSha256);
            tokenModel.ExpirationDate=DateTime.Now.AddMinutes(60);

            JwtSecurityToken securityToken = new //token ayarları
            (
                issuer:configuration["Token:Issuer"],
                audience:configuration["Token:Audience"],
                expires:tokenModel.ExpirationDate,
                notBefore:DateTime.Now,
                signingCredentials:credentials
            );
            //token yaratıldı.
            JwtSecurityTokenHandler tokenHandler= new JwtSecurityTokenHandler();
            tokenModel.AccessToken= tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken=CreateRefreshToken();
            return tokenModel;
        }
        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}