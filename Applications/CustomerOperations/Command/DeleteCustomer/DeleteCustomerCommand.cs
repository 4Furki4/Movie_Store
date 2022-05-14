using System;
using System.Linq;
using AutoMapper;
using MovieStore.DbOperations;

namespace MovieStore.Applications.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly MovieStoreDbContext context;
        public string CustomerE_Mail { get; set; }

        public DeleteCustomerCommand(MovieStoreDbContext context)
        {
            this.context = context;
        }



        public void Handler()
        {
            var customer = context.Customers.SingleOrDefault(c=>c.E_Mail==CustomerE_Mail);
            if(customer is null)
                throw new InvalidOperationException("Girmiş olduğunuz e-posta ile kayıtlı müşteri bulunmamaktadır!");
            context.Customers.Remove(customer);
            context.SaveChanges();
        }
    }
}