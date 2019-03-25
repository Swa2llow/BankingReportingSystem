using ReportingSystem.BLL.DTO;
using System;
using System.Collections.Generic;

namespace ReportingSystem.BLL.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<TransactionDTO> GetAll();
        IEnumerable<TransactionDTO> GetAll(int? creditCardId, DateTime? fromDate = null, DateTime? toDate = null);
        IEnumerable<TransactionDTO> GetByCard(int cardId);

        TransactionDTO Get(int id);
        void Create(TransactionDTO item);

        void Dispose();
    }
}
