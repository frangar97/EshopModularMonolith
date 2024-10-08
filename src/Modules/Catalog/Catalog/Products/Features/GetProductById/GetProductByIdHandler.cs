﻿namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductDto Product);
    internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(request.ProductId);
            }

            ProductDto productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
}
