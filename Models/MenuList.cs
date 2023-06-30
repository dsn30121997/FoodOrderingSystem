using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class MenuList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Required, MaxLength(30)]
        public string ItemName { get; set; }
        [Required, MaxLength(20)]
        public string Category { get; set; }
        [Required, MaxLength(20)]
        public string Subcategory { get; set; }
        [Required]
        public string Description { get; set; }
        [Required,DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public ICollection<Cart> Cart { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
    }
}