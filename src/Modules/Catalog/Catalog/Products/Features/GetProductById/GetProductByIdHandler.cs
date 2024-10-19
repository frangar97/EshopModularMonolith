using Catalog.Contracts.Products.Features.GetProductById;

namespace Catalog.Products.Features.GetProductById
{
    internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            ProductDto productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
}
