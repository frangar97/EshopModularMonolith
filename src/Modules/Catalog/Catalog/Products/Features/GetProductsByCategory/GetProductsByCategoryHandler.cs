namespace Catalog.Products.Features.GetProductsByCategory
{
    public record GetProducsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<ProductDto> products);

    internal class GetProductsByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProducsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProducsByCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await dbContext.Products
                .AsNoTracking()
                .Where(x => x.Category.Contains(request.category))
                .ToListAsync(cancellationToken);

            List<ProductDto> productsDto = products.Adapt<List<ProductDto>>();

            return new GetProductsByCategoryResult(productsDto);
        }
    }
}
