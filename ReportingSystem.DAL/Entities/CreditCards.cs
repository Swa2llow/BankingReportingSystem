using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportingSystem.DAL.Entities
{
    public class CreditCards
    {
        public CreditCards()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string CardHolderName { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal AvailableCash { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}