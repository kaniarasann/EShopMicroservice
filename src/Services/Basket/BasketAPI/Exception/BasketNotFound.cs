using BuildingBlocks.Exceptions;

namespace BasketAPI.Exception
{
    public class BasketNotFound : NotFoundException
    {
        public BasketNotFound(string username) : base("Basked",username)
        {
        }
    }
}
