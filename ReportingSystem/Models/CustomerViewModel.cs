using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportingSystem.Models
{
    public class CustomerViewModel
    {
        [NotMapped]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Firts name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(250)]
        public string Address { get; set; }
    }
}