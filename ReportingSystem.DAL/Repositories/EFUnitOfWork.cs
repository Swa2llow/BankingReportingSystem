using ReportingSystem.DAL.EF;
using ReportingSystem.DAL.Entities;
using ReportingSystem.DAL.Interfaces;
using System;

namespace ReportingSystem.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DBContext db;
        private CustomerRepository customerRepository;
        private CreditCardRepository cardRepository;
        private TransactionRepository transactionRepository;
        
        public EFUnitOfWork(string connectionString)
        {
            db = new DBContext(connectionString);
        }

        public IRepository<Customers> Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(db);
                return customerRepository;
            }
        }

        public IRepository<CreditCards> CreditCards
        {
            get
            {
                if (cardRepository == null)
                    cardRepository = new CreditCardRepository(db);
                return cardRepository;
            }
        }
        //public ICardRepository Cards
        //{
        //    get
        //    {
        //        if (cardRepository == null)
        //            cardRepository = new CreditCardRepository(db);
        //        return cardRepository;
        //    }
        //}

        public ITransactionRepository Transactions
        {
            get
            {
                if (transactionRepository == null)
                    transactionRepository = new TransactionRepository(db);
                return transactionRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
