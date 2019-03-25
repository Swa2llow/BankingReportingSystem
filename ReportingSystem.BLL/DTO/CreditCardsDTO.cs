using System;
using System.Collections.Generic;

namespace ReportingSystem.BLL.DTO
{
    public class CreditCardsDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public decimal AvailableCash { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class CustomerCreditCardsView
    {
        public List<CustomerCreditCards> Customers { get; set; }
    }

    public class CustomerCreditCards
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public List<CreditCardView> CreditCards { get; set; }
    }

    public class CreditCardView
    {
        public int CreditCardId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public decimal AvailableCash { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
