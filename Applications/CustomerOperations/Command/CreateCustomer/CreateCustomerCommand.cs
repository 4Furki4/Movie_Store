using System;
using System.Linq;
using AutoMapper;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Applications.CustomerOperations.Command.CreateCustomer
{
    public class CreateCustomerCommand
    {
        public CreateCustomerModel Model { get; set; }
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;

        public CreateCustomerCommand(MovieStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void Handler()
        {
            var customer= context.Customers.SingleOrDefault(c=>c.E_Mail==Model.E_Mail);
            if(customer is not null)
                throw new InvalidOperationException("Girdiğiniz kullanıcı zaten mevcut!");
            customer = mapper.Map<Customer>(Model);
            context.Customers.Add(customer);
        }
    }
    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string E_Mail { get; set; }

    }
}