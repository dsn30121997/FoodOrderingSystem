using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required, MaxLength(50)]
        public string CustomerName { get; set; }
        [Required,DataType(DataType.EmailAddress) , DatabaseGenerated(DatabaseGeneratedOption.None), MaxLength(30)]
        public string Email { get; set; }
        [Required,DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required,DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set; }

        public ICollection<Cart> Carts { get; set; }

        
    }
}