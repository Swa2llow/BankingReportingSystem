using ReportingSystem.BLL.DTO;
using System.Collections.Generic;

namespace ReportingSystem.BLL.Interfaces
{
    public interface ICustomerService
    {
        CustomerDTO GetCustomer(int id);
        IEnumerable<CustomerDTO> GetAllCustomers();
        void CreateCustomer(CustomerDTO customer);
        void Update(CustomerDTO customer);
        void Delete(CustomerDTO customer);
        void Dispose();
    }
}
