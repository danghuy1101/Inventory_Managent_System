using Application.DTO.Response;
using Application.DTO.Request.Products;
using MediatR;
namespace Application.Service.Products.Commands.Locations
{
    public record CreateLocationCommand(AddLocationRequestDTO LocationModel) : IRequest<ServiceResponse>;
}
