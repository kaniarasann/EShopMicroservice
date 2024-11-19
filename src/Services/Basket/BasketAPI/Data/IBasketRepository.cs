namespace BasketAPI.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCart(string Username,CancellationToken token);

        Task<ShoppingCart> SaveShoppingCart(ShoppingCart cart, CancellationToken token);

        Task<bool> DeleteShoppingCart(string Username, CancellationToken token);
    }
}
