
using BasketAPI.Exception;
using Marten;

namespace BasketAPI.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteShoppingCart(string Username, CancellationToken token)
        {
            session.Delete<ShoppingCart>(Username);
            await session.SaveChangesAsync(token);
            return true;
        }

        public async Task<ShoppingCart> GetShoppingCart(string Username, CancellationToken token)
        {
            var basket = await session.LoadAsync<ShoppingCart>(Username,token);
            if (basket == null) {
                throw new BasketNotFound(Username);
            }
            return basket;
        }

        public async Task<ShoppingCart> SaveShoppingCart(ShoppingCart cart, CancellationToken token)
        {
            session.Store(cart);
            await session.SaveChangesAsync(token);
            return cart;
        }
    }
}
