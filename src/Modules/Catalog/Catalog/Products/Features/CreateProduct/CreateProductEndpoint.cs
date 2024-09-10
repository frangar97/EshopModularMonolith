namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductRequest(ProductDto Product);
    public record CreateProductResponse(Guid id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                CreateProductCommand command = request.Adapt<CreateProductCommand>();

                CreateProductResult result = await sender.Send(command);

                CreateProductResponse response = result.Adapt<CreateProductResponse>();

                return Results.Created($"products/{response.id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
