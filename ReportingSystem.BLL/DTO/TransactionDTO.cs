using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingSystem.BLL.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
