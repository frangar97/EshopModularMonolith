namespace Catalog.Products.Features.GetProductsByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                GetProductsByCategoryResult result = await sender.Send(new GetProductsByCategoryQuery(category));

                GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
        }
    }
}
