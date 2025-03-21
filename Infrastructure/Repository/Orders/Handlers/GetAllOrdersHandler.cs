using Application.DTO.Response.Orders;
using Application.Extension.Identity;
using Application.Service.Orders;
using Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllOrdersHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory, UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllOrdersQuery, IEnumerable<GetOrderResponseDTO>> 
    {
        public async Task<IEnumerable<GetOrderResponseDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            using var dbContext = contextFactory.CreateDbContext();
            var orders = await dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            var products = await dbContext.Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            var users = await userManager.Users.ToListAsync(cancellationToken: cancellationToken);
            return orders.Select(order => new GetOrderResponseDTO
            {
                Id = order.Id,
                ProductName = products.FirstOrDefault(_ => _.Id == order.ProductId).Name,
                Price = order.Price,
                DeliveringDate = order.DeliveringDate,
                OrderedDate = order.DateOrdered,
                ProductId = order.ProductId,
                ProductImage = products.FirstOrDefault(_ => _.Id == order.ProductId).Base64Image,
                Quantity = order.Quantity,
                SerialNumber = products.FirstOrDefault(_ => _.Id == order.ProductId).SerialNumber,
                State = order.OrderState,
                CliendId = order.ClientId,
                CliendName = users.FirstOrDefault(_ => _.Id.Equals(order.ClientId)).Name
            }).ToList();
        }
    }
}
