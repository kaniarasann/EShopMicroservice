
using Marten.Pagination;

namespace CatalogAPI.Products.GetProduct
{
    public record GetProductQuery(int? pageSize = 5, int? pageNumber = 1) :IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(request.pageNumber ?? 1,request.pageSize ?? 5, cancellationToken);

            return new GetProductResult(products);

        }
    }
}
