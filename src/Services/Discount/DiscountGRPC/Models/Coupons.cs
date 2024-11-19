namespace DiscountGRPC.Models
{
    public class Coupons
    {
        public int Id {  get; set; }

        public string ProductName { get; set; } = default!;

        public string ProductDescription { get; set; } = default!;

        public decimal Amount { get; set; }
    }
}
