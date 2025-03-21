using Application.DTO.Response;
using Application.DTO.Request.Products;
using MediatR;
namespace Application.Service.Products.Commands.Categories
{
    public record CreateCategoryCommand(AddCategoryRequestDTO CategoryModel) : IRequest<ServiceResponse>;
}
