namespace Catalog.Products.Features.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<ProductDto> Products);

    internal class GetProductsByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await dbContext.Products
                .AsNoTracking()
                .Where(x => x.Category.Contains(request.Category))
                .ToListAsync(cancellationToken);

            List<ProductDto> productsDto = products.Adapt<List<ProductDto>>();

            return new GetProductsByCategoryResult(productsDto);
        }
    }
}
