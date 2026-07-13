using Ordering.Application.Orders.Queries.GetOrdersByStatus;
using Ordering.Domain.Enums;

namespace Ordering.API.Endpoints;

public record GetOrdersByStatusResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByStatus : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/status/{status}", async (OrderStatus status, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByStatusQuery(status));

            var response = result.Adapt<GetOrdersByStatusResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByStatus")
        .Produces<GetOrdersByStatusResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders By Status")
        .WithDescription("Get Orders By Status");
    }
}
