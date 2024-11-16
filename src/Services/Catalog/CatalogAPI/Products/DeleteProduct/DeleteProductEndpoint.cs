namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductResponse(bool isSucess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/DeleteProduct/{id}", async (Guid id, ISender sender) => {
               var response = await sender.Send(new DeleteProductCommand(id));
               return Results.Ok(response);
            }).WithName("Delete Product")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete product")
            .WithDescription("Delete product");
        }
    }
}
