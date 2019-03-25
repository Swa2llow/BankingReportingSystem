using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.DAL.Entities
{
    public class Customers
    {
        public Customers()
        {
            CreditCards = new HashSet<CreditCards>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        public virtual ICollection<CreditCards> CreditCards { get; set; }
    }
}