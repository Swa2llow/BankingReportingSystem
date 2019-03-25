using System;
using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Models
{
    public class EditCreditCardViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public int CardId { get; set; }
        
        [StringLength(250)]
        public string CardHolderName { get; set; }

        public decimal AvailableCash { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}