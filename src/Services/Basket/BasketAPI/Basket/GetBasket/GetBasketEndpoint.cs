namespace BasketAPI.Basket.GetBasket
{
    //public record GetBasketRequest(string username);
    public record GetBasketResponse(ShoppingCart cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/GetBasket/{username}",async (string username,ISender sender) => {
                var req = new GetBasketQuery(username);
                var res = await sender.Send(req);
                var response = res.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("Get Basket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket"); 
        }
    }
}
