using ReportingSystem.DAL.Entities;
using System;

namespace ReportingSystem.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customers> Customers { get; }
        IRepository<CreditCards> CreditCards { get; }
        //ICardRepository Cards { get; }
        ITransactionRepository Transactions { get; }
        void Save();
    }
}
