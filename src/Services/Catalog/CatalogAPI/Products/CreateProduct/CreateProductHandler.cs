namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is requried");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");

        }
    }
    public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product() { Name = command.Name, Description = command.Description, Price = command.Price, Category = command.Category };

            session.Store(product);

            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
