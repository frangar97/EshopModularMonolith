namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products.FindAsync([request.Product.Id], cancellationToken);

            if (product == null)
            {
                throw new Exception($"Product not found: {request.Product.Id}");
            }

            UpdateProductWithNewValues(product, request.Product);

            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product product, ProductDto productDto)
        {
            product.Update(
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price);
        }
    }
}
