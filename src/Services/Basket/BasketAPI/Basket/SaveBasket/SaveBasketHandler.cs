
using BasketAPI.Data;

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

    public class SaveBasketCommandHandler(IBasketRepository basket) : ICommandHandler<SaveBasketCommand, SaveBasketResult>
    {
        public async Task<SaveBasketResult> Handle(SaveBasketCommand request, CancellationToken cancellationToken)
        {
           var response = await basket.SaveShoppingCart(request.cart, cancellationToken);
           return new SaveBasketResult(response.UserName);
        }
    }
}
