using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions
{
    [Serializable]
    internal class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id)
        {
        }

       
    }
}