using CatalogAPI.Products.GetProductByCategory;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category) : ICommand<UpdateProductResult>;

    public record UpdateProductResponse(bool isSucess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/UpdateProduct", async(UpdateProductRequest request,ISender sender)=>{

                var res = request.Adapt<UpdateProductCommand>();
                var rep = await sender.Send(res);
                var response = rep.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            }).WithName("Update Product")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update product")
            .WithDescription("Update product");
        }
    }
}
