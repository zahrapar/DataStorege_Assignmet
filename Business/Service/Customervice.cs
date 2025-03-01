using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service;




public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    //public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    //{
    //    var customerEntity = new CustomerEntity
    //    {
    //        CustomerName = form.CustomerName
    //    };
    //    await _customerRepository.AddAsync(customerEntity);
    //}
    public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var existingCustomer = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        if (existingCustomer == null)
        {
            var customerEntity = CustomerFactory.Create(form);
            await _customerRepository.AddAsync(customerEntity!);
            Console.WriteLine("Customer created successfully.");
        }
        else
        {
            Console.WriteLine("Customer already exists.");
        }
    }

    //public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    //{
    //    //await _customerRepository.AddAsync(CustomerFactory.Create(form));

    //    var customerEntity = CustomerFactory.Create(form);
    //    await _customerRepository.AddAsync(customerEntity!);
    //}


    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAsync();
        return customerEntities.Select(CustomerFactory.Create);
    }


    //public async Task<IEnumerable<Customer>> GetCustomersAsync()
    //{
    //    var customerEntities = await _customerRepository.GetAsync();
    //    var customers = new List<Customer>();
    //    foreach (var entity in customerEntities)
    //    {
    //        customers.Add(new Customer
    //        {
    //            Id = entity.Id,
    //            CustomerName = entity.CustomerName
    //        });
    //    }
    //    return customers;
    //}


    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity);
    }


    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity);
    }

    //public async Task<bool> UpdateCustomerAsync(Customer customer) { }

    //public async Task<bool> DeleteCustomerAsync(int id) { }
}

