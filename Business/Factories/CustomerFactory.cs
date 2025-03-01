using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistrationForm? form) =>
        form == null ? null : new CustomerEntity
        {
            CustomerName = form.CustomerName
        };

    public static Customer? Create(CustomerEntity? entity) =>
        entity == null ? null : new Customer
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName
        };
}
