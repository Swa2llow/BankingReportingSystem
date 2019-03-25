using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingSystem.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class CustomerTransactionViewModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public List<CreditTransactionViewModel> CreditCards { get; set; }
    }

    public class CreditTransactionViewModel
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public decimal AvailableCash { get; set; }
        public DateTime ExpirationDate { get; set; }

        public List<TransactionViewModel> Transaction { get; set; }
    }
}