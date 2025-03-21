﻿using Application.DTO.Response;
using Application.Service.Products.Commands.Locations;
using Infrastructure.DataAccess;
using Domain.Entities.Products;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repository.Products.Handlers.Locations
{
    public class UpdateLocationHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<UpdateLocationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var dbContext = contextFactory.CreateDbContext();
                var category = await dbContext.Locations.FirstOrDefaultAsync(_ => _.Id.Equals(request.LocationModel.Id), cancellationToken: cancellationToken);
                if (category == null)
                    return GeneralDbResponses.ItemNotFound(request.LocationModel.Name);

                dbContext.Entry(category).State = EntityState.Detached;

                var adaptData = request.LocationModel.Adapt(new Location());
                dbContext.Locations.Update(adaptData);
                await dbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdate(request.LocationModel.Name);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(true, ex.Message);
            }
        }
    }
}
