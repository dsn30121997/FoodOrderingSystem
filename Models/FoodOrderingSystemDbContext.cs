using System.Data.Entity;

namespace FoodOrderingSystem.Models
{
    public class FoodOrderingSystemDbContext : DbContext
    {
        public FoodOrderingSystemDbContext() : base("sqlcon")
        { }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<MenuList> MenuList { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasKey(c => new { c.CustomerId, c.ItemId });

            modelBuilder.Entity<OrderItems>().HasKey(oi => new { oi.OrderId, oi.ItemId, oi.Quantity });
            modelBuilder.Entity<Payment>().HasKey(p => new { p.OrderId, p.TotalAmount });


            base.OnModelCreating(modelBuilder);
        }
    }
}