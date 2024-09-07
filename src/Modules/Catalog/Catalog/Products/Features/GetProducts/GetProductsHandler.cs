namespace Catalog.Products.Features.GetProducts
{
    public record GetProdutsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductDto> Products);
    internal class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProdutsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProdutsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            List<ProductDto> productsDto = products.Adapt<List<ProductDto>>();

            return new GetProductsResult(productsDto);
        }
    }
}
