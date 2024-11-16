namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryRequest(IEnumerable<string> category);

    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/GetProductByCategory", async (GetProductByCategoryRequest request, ISender sender) => {
                //var req = request.Adapt<GetProductByCategoryQuery>();
                var res = await sender.Send(new GetProductByCategoryQuery(category:request.category));
                var resp = res.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(resp);
            }).WithName("Get Product By Category")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
        }
    }
}
