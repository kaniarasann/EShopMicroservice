namespace BasketAPI.Models
{
    public class ShoppingCart
    {
        public ShoppingCart() {}      

        public ShoppingCart(string username) => this.UserName = username;

        public string UserName {  get; set; }   

        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
