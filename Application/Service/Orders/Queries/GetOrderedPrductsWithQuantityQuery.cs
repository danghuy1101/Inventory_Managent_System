using Application.DTO.Response.Orders;
using MediatR;
namespace Application.Service.Orders
{
    public record GetOrderedPrductsWithQuantityQuery(string UserId = null) : IRequest<IEnumerable<GetOrderedPrductsWithQuantityResponseDTO>>;
}
