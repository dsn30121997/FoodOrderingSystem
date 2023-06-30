using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }
        [ MaxLength(50)]
        public string Name { get; set; }
        [Required,DataType(DataType.EmailAddress), MaxLength(30)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }
        [Required, DataType(DataType.Password),MaxLength(20)]
        public string Password { get; set; }


    }
}