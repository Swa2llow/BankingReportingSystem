using ReportingSystem.DAL.Entities;
using System.Collections.Generic;

namespace ReportingSystem.DAL.Interfaces
{
    public interface ICardRepository
    {
        IEnumerable<CreditCards> GetCardByCustomerId(int customerId);
    }
}
