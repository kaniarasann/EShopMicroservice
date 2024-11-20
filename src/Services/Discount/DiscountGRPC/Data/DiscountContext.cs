using DiscountGRPC.Models;
using DiscountGRPC.Protos;
using Microsoft.EntityFrameworkCore;

namespace DiscountGRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupons> Coupon { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> context) : base( context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Coupons>().HasData(
               new Coupons { Id = 1, ProductName ="Iphone 14 Pro" , ProductDescription = "iphone black friday offer" , Amount = 100},
               new Coupons { Id = 2, ProductName = "Samsung 14", ProductDescription = "samsung black friday offer", Amount = 80 }
           );
        }

    }
}
