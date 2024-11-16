namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, List<string> Category);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) => {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("Create Product")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
