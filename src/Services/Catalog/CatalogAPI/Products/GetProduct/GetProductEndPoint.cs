namespace CatalogAPI.Products.GetProduct
{
    public record GetProductRequest(int? pageSize = 5,int? pageNumber = 1);
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/getproducts", async ([AsParameters] GetProductRequest request,ISender sender) => {
                var req = request.Adapt<GetProductQuery>();
                var result = await sender.Send(req);
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
            .WithName("Get Products")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
