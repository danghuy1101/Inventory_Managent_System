using WebUI.Components;
using Infrastructure.DependencyInjection;
using Application.DependencyInjection;
using WebUI.Components.Layout.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using WebUI.States;
using Syncfusion.Blazor;
using MudBlazor.Services;
using NetcodeHub.Packages.Components.DataGrid;
using WebUI.States.User;
using WebUI.Hubs;
using WebUI.States.Adminstration;
using NetcodeHub.Packages.Extensions.LocalStorage;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthStateProvider>();
builder.Services.AddSingleton<ChangePasswordState>();
builder.Services.AddScoped<UserActiveOrderCountState>();
builder.Services.AddScoped<AdminActiveOrderCountState>();
builder.Services.AddScoped<GenericHomeHeaderState>();
builder.Services.AddScoped<NetcodeHubConnectionService>();
builder.Services.AddScoped<ICustomAuthorizationService, CustomAuthorizationService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddVirtualizationService();
builder.Services.AddNetcodeHubLocalStorageService();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");
builder.Services.AddMudServices();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapSignOutEnpoint();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true
});
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapHub<CommunicationHub>("/communicationhub");
app.Run();
