using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [Required, MaxLength(50)]
        public string EmployeeName { get; set; }
        [Required, DataType(DataType.EmailAddress), MaxLength(30)]
        public string Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public  long Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set; }


    }
}