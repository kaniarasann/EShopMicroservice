
using BasketAPI.Basket.SaveBasket;

namespace BasketAPI.Basket.DeleteBasket
{
    //public record DeleteBasketRequest();
    public record DeleteBasketResponse(bool success);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/DeleteBasket/{Username}", async (string username, ISender sender) => {
                var req = new DeleteBasketCommand(username);
                var res = await sender.Send(req);
                var response = res.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("Delete Basket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket");
        }
    }
}
