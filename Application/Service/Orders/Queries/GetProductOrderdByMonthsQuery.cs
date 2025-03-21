using Application.DTO.Response.Orders;
using MediatR;
namespace Application.Service.Orders
{
    public record GetProductOrderdByMonthsQuery(string UserId = null) : IRequest<IEnumerable<GetProductOrderdByMonthsResponseDTO>>;
}
