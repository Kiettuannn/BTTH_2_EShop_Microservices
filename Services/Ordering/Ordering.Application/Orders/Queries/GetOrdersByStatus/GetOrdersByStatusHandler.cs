using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.Queries.GetOrdersByStatus;

public class GetOrdersByStatusHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByStatusQuery, GetOrdersByStatusResult>
{
    public async Task<GetOrdersByStatusResult> Handle(GetOrdersByStatusQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                        .Include(o => o.OrderItems)
                        .AsNoTracking()
                        .Where(o => o.Status == query.Status)
                        .OrderBy(o => o.OrderName.Value)
                        .ToListAsync(cancellationToken);

        return new GetOrdersByStatusResult(orders.ToOrderDtoList());
    }
}
