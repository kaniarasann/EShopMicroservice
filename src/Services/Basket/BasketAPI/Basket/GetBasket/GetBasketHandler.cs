using BasketAPI.Data;

namespace BasketAPI.Basket.GetBasket
{
    public record GetBasketQuery(string username) : IQuery<GetBaksetResult>;

    public record GetBaksetResult(ShoppingCart cart);
    public class GetBasketQueryHandler(IBasketRepository repo) : IQueryHandler<GetBasketQuery, GetBaksetResult>
    {
        public async Task<GetBaksetResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var result = await repo.GetShoppingCart(request.username, cancellationToken);
            return  new GetBaksetResult(result);
        }
    }
}
