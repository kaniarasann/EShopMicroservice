namespace CatalogAPI.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            
            app.MapGet("/getproductsbyid/{id:Guid}", async ( Guid Id,ISender sender) => {
                var result = await sender.Send(new GetProductByIdQuery(Id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("Get Products By Id")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get Products by Id")
            .WithDescription("Get Products by Id");
        }
    }
}
