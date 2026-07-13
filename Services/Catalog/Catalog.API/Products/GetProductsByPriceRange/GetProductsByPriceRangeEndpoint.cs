namespace Catalog.API.Products.GetProductsByPriceRange;

public record GetProductsByPriceRangeResponse(IEnumerable<Product> Products);

public class GetProductsByPriceRangeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/price", async (decimal minPrice, decimal maxPrice, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByPriceRangeQuery(minPrice, maxPrice));

            var response = result.Adapt<GetProductsByPriceRangeResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductsByPriceRange")
        .Produces<GetProductsByPriceRangeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Price Range")
        .WithDescription("Get Products By Price Range");
    }
}
