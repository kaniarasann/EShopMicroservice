
namespace BasketAPI.Basket.SaveBasket
{
    public record SaveBasketRequest(ShoppingCart cart);
    public record SaveBasketResponse(string username);
    public class SaveBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/SaveBasket", async (SaveBasketRequest request, ISender sender) =>
            {
                var req = request.Adapt<SaveBasketCommand>();
                var response = await sender.Send(req);
                var res = response.Adapt<SaveBasketResponse>();
                return Results.Created($"/basket/{response.username}",res);
            })
            .WithName("Save Basket")
            .Produces<SaveBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Save Basket")
            .WithDescription("Save Basket");
        }
    }
}
