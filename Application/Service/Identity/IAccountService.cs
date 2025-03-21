﻿using Application.DTO.Response;
using Application.DTO.Response.ActivityTracker;
using Application.DTO.Response.Identity;
using Application.DTO.Request.ActivityTracker;
using Application.DTO.Request.Identity;
using Microsoft.Extensions.Logging;
namespace Application.Service.Identity
{
    public interface IAccountService
    {
        Task<ServiceResponse> LoginAsync(LoginUserRequestDTO model);
        Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO model);
        Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUsersWithClaimsAsync();
        Task SetUpAsync();
        Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model);
        Task SaveActivityAsync(ActivityTrackerRequestDTO model);
        Task<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDTO>>> GroupActivitiesByDateAsync();
    }
}
