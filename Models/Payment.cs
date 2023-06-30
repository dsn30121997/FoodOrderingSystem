using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Payment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        //[ForeignKey("Orders"), Column(Order = 1)]
        public int OrderId { get; set; }
        //[ForeignKey("Orders"), Column(Order = 2)]
        [DataType(DataType.Currency),Required]
        public decimal TotalAmount { get; set; }
        [Required,MaxLength(15)]
        public string PaymentMethod { get; set; }
        [Required, DataType(DataType.DateTime),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime PaymentDate { get; set; }


        public Orders Orders { get; set; }

    }
}