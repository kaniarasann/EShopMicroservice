
using BasketAPI.Basket.SaveBasket;
using BasketAPI.Data;

namespace BasketAPI.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username):ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool Success);

    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can't be empty");
        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository basket) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
          await basket.DeleteShoppingCart(request.Username, cancellationToken);
          return new DeleteBasketResult(true);
        }
    }
}
