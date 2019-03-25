using ReportingSystem.DAL.Entities;
using System;
using System.Collections.Generic;

namespace ReportingSystem.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transactions> GetAll();
        IEnumerable<Transactions> GetByCard(int cardId);

        Transactions Get(int id);

        IEnumerable<Transactions> Find(Func<Transactions, Boolean> predicate);

        void Create(Transactions item);
    }
}
