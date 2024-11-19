
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BasketAPI.Data
{
    public class CacheBasketRepository(IBasketRepository repository,IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteShoppingCart(string Username, CancellationToken token)
        {          
            var res = await repository.DeleteShoppingCart(Username, token);
            await cache.RemoveAsync(Username, token);
            return res;
        }

        public async Task<ShoppingCart> GetShoppingCart(string Username, CancellationToken token)
        {
            var cached = await cache.GetStringAsync(Username, token);
            if (cached != null)
                return JsonConvert.DeserializeObject<ShoppingCart>(cached)!;
            else
            {
                var basket = await repository.GetShoppingCart(Username, token);
                await cache.SetStringAsync(Username,JsonConvert.SerializeObject(basket));
                return basket;
            }
        }

        public async Task<ShoppingCart> SaveShoppingCart(ShoppingCart cart, CancellationToken token)
        {
            var result = await repository.SaveShoppingCart(cart, token);
            await cache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(result));
            return result;
        }
    }
}
