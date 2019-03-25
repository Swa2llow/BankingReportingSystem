using AutoMapper;
using ReportingSystem.BLL.DTO;
using ReportingSystem.BLL.Infrastructure;
using ReportingSystem.BLL.Interfaces;
using ReportingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReportingSystem.Controllers
{
    public class CustomerApiController : ApiController
    {
        ICustomerService customerService;

        public CustomerApiController(ICustomerService service)
        {
            customerService = service;
        }

        [HttpGet]
        public IHttpActionResult GetAllCustomers()
        {
            var customerDto = customerService.GetAllCustomers();
            if (customerDto == null || customerDto.ToList().Count == 0)
                return BadRequest("Customer not found.");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>().ForMember(c => c.CustomerId, m => m.MapFrom(s => s.Id))).CreateMapper();
            var customer = mapper.Map<IEnumerable<CustomerDTO>, List<CustomerViewModel>>(customerDto);
            
            return Ok(customer);
        }
        
        [HttpGet, ActionName("GetCustomerById")]
        public IHttpActionResult GetCustomerById(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid customer id");

            var customerDto = customerService.GetCustomer(id);
            if (customerDto == null)
                return BadRequest("Customer not found.");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>().ForMember(c => c.CustomerId, m => m.MapFrom(s => s.Id))).CreateMapper();
            var customer = mapper.Map<CustomerDTO, CustomerViewModel>(customerDto);

            return Ok(customer);
        }

        // POST: api/CustomerApi
        public IHttpActionResult Post([FromBody]CustomerViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (value is null)
                return BadRequest("Parameter type is null.");
                        
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerViewModel, CustomerDTO>()).CreateMapper();
            var model = mapper.Map<CustomerViewModel, CustomerDTO>(value);
            
            customerService.CreateCustomer(model);
            return Ok();
        }

        // PUT: api/CustomerApi/5
        public IHttpActionResult Put([FromBody]EditCustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (customer is null)
                return BadRequest("Parameter type is null.");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EditCustomerViewModel, CustomerDTO>().ForMember(c => c.Id, m => m.MapFrom(s => s.CustomerId))).CreateMapper();
            var customerDto = mapper.Map<EditCustomerViewModel, CustomerDTO>(customer);

            try
            {
                customerService.Update(customerDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/CustomerApi/5
        public IHttpActionResult Delete(int customerId)
        {
            if (customerId <= 0)
                return BadRequest("Not a valid customer id");

            var customerDto = customerService.GetCustomer(customerId);
            if (customerDto == null)
                return BadRequest("Customer not found.");

            customerService.Delete(customerDto);
            return Ok(customerDto);
        }
    }
}
