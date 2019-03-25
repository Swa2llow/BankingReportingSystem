using AutoMapper;
using ReportingSystem.BLL.DTO;
using ReportingSystem.BLL.Interfaces;
using ReportingSystem.DAL.Entities;
using ReportingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportingSystem.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        IUnitOfWork Database { get; set; }

        public TransactionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<TransactionDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Transactions, TransactionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Transactions>, IEnumerable<TransactionDTO>>(Database.Transactions.GetAll());
        }

            public IEnumerable<TransactionDTO> GetAll(int? creditCardId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var data = Database.Transactions.GetAll();
            
            DateTime start = default(DateTime);
            DateTime end = default(DateTime);

            if (fromDate.HasValue && toDate.HasValue)
            {                
                start = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 0, 0, 0);
                end = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, 0, 0, 0);
            }
            else{
                if (!fromDate.HasValue && !toDate.HasValue)
                {
                    start = DateTime.Now;
                    end = start.AddDays(30);
                }
                else
                {
                    if (fromDate.HasValue)
                    {
                        start = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 0, 0, 0);
                        end = start.AddDays(30);
                    }
                    else
                    {
                        end = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, 0, 0, 0);
                        start = end.AddDays(-30);
                    }
                }
            }


            var query = from tran in data
                    where (tran.CreationDate >= start && tran.CreationDate <= end)
                        orderby tran.Id
                    select tran;

            if (creditCardId.HasValue)
            {
                int cardId = creditCardId.Value;
                query.Where(t => t.CardId == cardId).ToList();
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Transactions, TransactionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Transactions>, IEnumerable<TransactionDTO>>(query);
        }

        public IEnumerable<TransactionDTO> GetByCard(int cardId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Transactions, TransactionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Transactions>, List<TransactionDTO>>(Database.Transactions.GetByCard(cardId));
        }

        public TransactionDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Transactions, TransactionDTO>()).CreateMapper();
            return mapper.Map<Transactions, TransactionDTO>(Database.Transactions.Get(id));
        }

        public void Create(TransactionDTO tr)
        {

            Transactions newCard = new Transactions
            {
                CustomerId = tr.CustomerId,
                CardId = tr.CardId,
                Amount = tr.Amount,
                Status = tr.Status,
                Message= tr.Message,
                CreationDate = tr.CreationDate
            };

            Database.Transactions.Create(newCard);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
