using Application.DTO.Response.Orders;
using Application.Service.Orders;
using Domain.Entities.Orders;
using Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetOrderedPrductsWithQuantityHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<GetOrderedPrductsWithQuantityQuery, IEnumerable<GetOrderedPrductsWithQuantityResponseDTO>>
    {
        public async Task<IEnumerable<GetOrderedPrductsWithQuantityResponseDTO>> Handle(GetOrderedPrductsWithQuantityQuery request, CancellationToken cancellationToken)
        {
            using var dbContext = contextFactory.CreateDbContext();
            var orders = new List<Order>();
            var data = new List<GetOrderedPrductsWithQuantityResponseDTO>();
            if (!string.IsNullOrEmpty(request.UserId)) 
                orders = await dbContext.Orders.AsNoTracking().Where(_ => _.ClientId.ToString() == request.UserId).ToListAsync(cancellationToken: cancellationToken);
            else
                orders = await dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

            var selectOrdersWithin12Months = orders
                                             .Where(order => order.DateOrdered.Date >= DateTime.Today.AddMonths(-12))
                                             .GroupBy(orders => new { Name = orders.ProductId })
                                             .ToList();

            foreach (var order in selectOrdersWithin12Months)
            {
                data.Add(new GetOrderedPrductsWithQuantityResponseDTO()
                {
                    ProductName = (await dbContext.Products.FirstOrDefaultAsync(_ => _.Id == order.Key.Name, cancellationToken: cancellationToken)).Name,
                    QuantityOrdered = order.Sum(x => x.Quantity),
                });
            }
            return data;
        }
    }
}
