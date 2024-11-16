namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(IEnumerable<string> category) :IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = new List<Product>();
            foreach (var item in request.category)
            {
                var res = await session.Query<Product>()
                .Where(x => x.Category.Contains(item)).ToListAsync();
                foreach (var item1 in res)
                {
                    products.Add(item1);
                }
            }
            return new GetProductByCategoryResult(products);
        }
    }
}
