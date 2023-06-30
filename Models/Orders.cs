using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Orders
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        //[ForeignKey("Cart"), Column(Order = 1)]
        public int CustomerId { get; set; }
        //[ForeignKey("Cart"), Column(Order = 5)]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
        [DataType(DataType.DateTime), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderDate { get; set; }
        [MaxLength(20)]
        public string OrderStatus { get; set; }
        public string SpicialInstruction { get; set; }


        public Customer Customer { get; set; }
        public  Cart Cart { get; set; }    
        public ICollection<OrderItems> OrderItems { get; set; }
        
    }
}