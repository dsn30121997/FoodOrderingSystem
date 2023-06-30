using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Cart
    {
        [Key]
        public int SerialId { get; set; }
       // [ForeignKey("Customer"),Column(Order = 1)]
        public int CustomerId { get; set; }
        //[ForeignKey("MenuList"), Column(Order = 2)]
        public int ItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required,DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        public  Customer Customer { get; set; }
        public MenuList MenuList { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }


    }
}