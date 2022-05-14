using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string E_Mail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string RefresToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public List<Order> Order { get; set; }
    }
}