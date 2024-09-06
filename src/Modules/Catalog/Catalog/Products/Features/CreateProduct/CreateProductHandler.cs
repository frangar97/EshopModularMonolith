namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto product) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);
    internal class CreateProducHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = CreateNewProduct(request.product);

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