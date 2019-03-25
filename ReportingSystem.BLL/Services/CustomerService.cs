using AutoMapper;
using ReportingSystem.BLL.DTO;
using ReportingSystem.BLL.Infrastructure;
using ReportingSystem.BLL.Interfaces;
using ReportingSystem.DAL.Entities;
using ReportingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportingSystem.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        IUnitOfWork Database { get; set; }

        public CustomerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Customers, CustomerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Customers>, List<CustomerDTO>>(Database.Customers.GetAll());
        }

        public CustomerDTO GetCustomer(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Customers, CustomerDTO>()).CreateMapper();
            return mapper.Map<Customers, CustomerDTO>(Database.Customers.Get(id));
        }

        public void CreateCustomer(CustomerDTO customerDto)
        {
            Customers customer = Database.Customers.GetAll().Where(c=>c.Email.Contains(customerDto.Email)).FirstOrDefault();
            
            if (customer != null)
                throw new ValidationException($"Customer with {customerDto.Email} exists", "");
            
            Customers newCustomer = new Customers
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address
            };

            Database.Customers.Create(newCustomer);
            Database.Save();
        }
        
        public void Update(CustomerDTO customerDto)
        {
            Customers customer = Database.Customers.Get(customerDto.Id);

            if (customer == null)
                throw new ValidationException($"Customer with {customerDto.Id} not exists", "");

            if (customer != null)
            {
                if(!String.IsNullOrEmpty(customerDto.FirstName))
                    customer.FirstName = customerDto.FirstName;

                if (!String.IsNullOrEmpty(customerDto.LastName))
                    customer.LastName = customerDto.LastName;

                if (!String.IsNullOrEmpty(customerDto.Email))
                    customer.Email = customerDto.Email;

                if (!String.IsNullOrEmpty(customerDto.Phone))
                    customer.Phone = customerDto.Phone;

                if (!String.IsNullOrEmpty(customerDto.Address))
                    customer.Address = customerDto.Address;
            }

            Database.Customers.Update(customer);
            Database.Save();
        }

        public void Delete(CustomerDTO customerDto)
        {
            Customers customer = Database.Customers.Get(customerDto.Id);

            if (customer == null)
                throw new ValidationException($"Customer with {customerDto.Id} not exists", "");

            Database.Customers.Delete(customer);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
