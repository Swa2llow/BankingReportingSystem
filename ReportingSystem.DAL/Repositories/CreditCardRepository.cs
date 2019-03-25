using ReportingSystem.DAL.EF;
using ReportingSystem.DAL.Entities;
using ReportingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReportingSystem.DAL.Repositories
{
    public class CreditCardRepository : IRepository<CreditCards>
    {
        private DBContext db;

        public CreditCardRepository(DBContext context)
        {
            this.db = context;
        }

        public IEnumerable<CreditCards> GetAll()
        {
            return db.CreditCards;
        }

        public CreditCards Get(int id)
        {
            return db.CreditCards.Find(id);
        }

        public IEnumerable<CreditCards> GetCardByCustomerId(int customerId)
        {
            return db.CreditCards.Where(c => c.CustomerId == customerId).ToList();
        }

        public void Create(CreditCards card)
        {
            db.CreditCards.Add(card);
        }

        public void Update(CreditCards card)
        {
            db.Entry(card).State = EntityState.Modified;
        }

        public void Delete(CreditCards card)
        {
                db.CreditCards.Remove(card);
        }

        public void Delete(int id)
        {
            CreditCards card = db.CreditCards.Find(id);
            if (card != null)
                db.CreditCards.Remove(card);

        }
    }
}
