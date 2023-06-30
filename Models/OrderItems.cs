using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class OrderItems
    {
        
        //[ForeignKey("Orders"), Column(Order = 1)]
        public int OrderId { get; set; }
        
        //[ForeignKey("Cart"),Column(Order = 3)]
        public int ItemId { get; set; }
        
        //[ForeignKey("Cart"), Column(Order = 4)]
        public int Quantity { get; set; }
        [MaxLength(20)]
        public string ItemStatus { get; set; }


        public  Orders Orders { get; set; }
        public  Cart Cart { get; set; }
        public  MenuList MenuList { get; set; }
    }
}