using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Models
{
    public class EditCustomerViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Address { get; set; }
    }
}