namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, string Description, string ImageFile, decimal Price, List<string> Category):ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool isSucess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is reuired");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }

    public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var res = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (res == null) {
                throw new ProductNotFoundException(request.Id);
            } 

            res.Name = request.Name;
            res.Description = request.Description;
            res.ImageFile = request.ImageFile;
            res.Price = request.Price;
            res.Category = request.Category;

            session.Update(res);
           await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
