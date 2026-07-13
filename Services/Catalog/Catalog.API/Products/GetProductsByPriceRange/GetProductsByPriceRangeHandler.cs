namespace Catalog.API.Products.GetProductsByPriceRange;

public record GetProductsByPriceRangeQuery(decimal MinPrice, decimal MaxPrice)
    : IQuery<GetProductsByPriceRangeResult>;
public record GetProductsByPriceRangeResult(IEnumerable<Product> Products);

public class GetProductsByPriceRangeQueryValidator : AbstractValidator<GetProductsByPriceRangeQuery>
{
    public GetProductsByPriceRangeQueryValidator()
    {
        RuleFor(x => x.MinPrice).GreaterThanOrEqualTo(0).WithMessage("MinPrice must be >= 0");
        RuleFor(x => x.MaxPrice).GreaterThan(0).WithMessage("MaxPrice must be greater than 0");
        RuleFor(x => x).Must(x => x.MaxPrice >= x.MinPrice)
            .WithMessage("MaxPrice must be >= MinPrice");
    }
}

internal class GetProductsByPriceRangeQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsByPriceRangeQuery, GetProductsByPriceRangeResult>
{
    public async Task<GetProductsByPriceRangeResult> Handle(GetProductsByPriceRangeQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Price >= query.MinPrice && p.Price <= query.MaxPrice)
            .ToListAsync(cancellationToken);

        return new GetProductsByPriceRangeResult(products);
    }
}
