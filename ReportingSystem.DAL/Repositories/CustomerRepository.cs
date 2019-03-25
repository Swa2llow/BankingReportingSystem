using ReportingSystem.DAL.EF;
using ReportingSystem.DAL.Entities;
using ReportingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReportingSystem.DAL.Repositories
{
    public class CustomerRepository : IRepository<Customers>
    {
        private DBContext db;

        public CustomerRepository(DBContext context) => this.db = context;

        public IEnumerable<Customers> GetAll()
        {
            var r = db.Customers.ToList();

            return db.Customers;
        }

        public Customers Get(int id) => db.Customers.Find(id);

        public void Create(Customers customer) => db.Customers.Add(customer);

        public void Update(Customers customer) => db.Entry(customer).State = EntityState.Modified;

        public void Delete(Customers customer) => db.Customers.Remove(customer);

        public void Delete(int id)
        {
            Customers customer = db.Customers.Find(id);
            if (customer != null)
                db.Customers.Remove(customer);
        }
    }
}
