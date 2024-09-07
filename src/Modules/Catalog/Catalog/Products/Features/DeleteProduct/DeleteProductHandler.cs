
namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid productId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products.FindAsync([request.productId], cancellationToken);

            if (product == null)
            {
                throw new Exception($"Product not found: {request.productId}");
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
