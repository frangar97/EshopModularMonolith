using FluentValidation;

namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class CreateProducHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = CreateNewProduct(request.Product);

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(ProductDto product)
        {
            return Product.Create(Guid.NewGuid(), product.Name, product.Category, product.Description, product.ImageFile, product.Price);
        }
    }
}