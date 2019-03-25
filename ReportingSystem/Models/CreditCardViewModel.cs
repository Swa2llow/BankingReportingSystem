using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReportingSystem.Models
{
    //public class CustomerCreditCardsCollection
    //{
    //    public int CustomerId { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Email { get; set; }
    //    public string Phone { get; set; }
    //    public string Address { get; set; }

    //    public List<CreditCardsView> CreditCards { get; set; }
    //}

    //public class CreditCardsView
    //{
    //    public int CardId { get; set; }
    //    public string CardNumber { get; set; }
    //    public string CardHolderName { get; set; }
    //    public decimal AvailableCash { get; set; }
    //    public DateTime ExpirationDate { get; set; }
    //}

    //public class CustomerCreditCardViewModel
    //{
    //    public List<CustomerViewModel> Customer { get; set; }
    //}
    
    public class CreditCardViewModel
    {
        [NotMapped]
        public int CardId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string CardHolderName { get; set; }
        
        [Required]
        public decimal AvailableCash { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }        
    }
}