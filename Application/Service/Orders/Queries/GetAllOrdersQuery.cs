﻿using Application.DTO.Response.Orders;
using MediatR;
namespace Application.Service.Orders
{
    public record GetAllOrdersQuery : IRequest<IEnumerable<GetOrderResponseDTO>>;
}
