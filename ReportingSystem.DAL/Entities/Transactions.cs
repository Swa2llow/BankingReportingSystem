using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportingSystem.DAL.Entities
{
    public class Transactions
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int CardId { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Amount { get; set; }

        public int Status { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual CreditCards CreditCard { get; set; }
    }
}
