namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id is required");
        }
    }
    internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products.FindAsync([request.ProductId], cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(request.ProductId);
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
