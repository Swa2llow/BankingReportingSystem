using ReportingSystem.DAL.EF;
using ReportingSystem.DAL.Entities;
using ReportingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace ReportingSystem.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private DBContext db;

        public TransactionRepository(DBContext context)
        {
            this.db = context;
        }

        public IEnumerable<Transactions> GetAll() => db.Transactions;

        public IEnumerable<Transactions> GetByCard(int cardId)
        {
            return db.Transactions.Where(t => t.CardId == cardId).ToList();
        }

        public Transactions Get(int id)
        {
            return db.Transactions.Find(id);
        }

        public void Create(Transactions tr) => db.Transactions.Add(tr);

        public IEnumerable<Transactions> Find(Func<Transactions, Boolean> predicate)
        {
            return db.Transactions.Include(o => o.CreditCard).Where(predicate).ToList();
        }
    }
}
