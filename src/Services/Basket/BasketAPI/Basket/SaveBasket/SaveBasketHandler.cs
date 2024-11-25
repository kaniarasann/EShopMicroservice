
using BasketAPI.Data;
using DiscountGRPC.Protos;

namespace BasketAPI.Basket.SaveBasket
{
    public record SaveBasketCommand(ShoppingCart cart) : ICommand<SaveBasketResult>;

    public record SaveBasketResult(string username);

    public class SaveBasketValidator : AbstractValidator<SaveBasketCommand>
    {
        public SaveBasketValidator()
        {
            RuleFor(x => x).NotNull().WithMessage("Cart can't be empty");
            RuleFor(x=>x.cart).NotNull().WithMessage("Cart can't be empty");
            RuleFor(x => x.cart.UserName).NotNull().NotEmpty().WithMessage("UserName is required");
        }
    }

    public class SaveBasketCommandHandler(IBasketRepository basket,DiscountProtoService.DiscountProtoServiceClient discountGrpc) : ICommandHandler<SaveBasketCommand, SaveBasketResult>
    {
        public async Task<SaveBasketResult> Handle(SaveBasketCommand request, CancellationToken cancellationToken)
        {
            foreach (ShoppingCartItem item in request.cart.Items)
            {
              var disamt = await discountGrpc.GetDiscountAsync( new GetDiscountRequest() { ProductName = item.ProductName } );
              item.Price = item.Price - disamt.Amount;
            }

            request.cart.TotalPrice = request.cart.Items.Sum(x => x.Price);

            var response = await basket.SaveShoppingCart(request.cart, cancellationToken);
           return new SaveBasketResult(response.UserName);
        }
    }
}
