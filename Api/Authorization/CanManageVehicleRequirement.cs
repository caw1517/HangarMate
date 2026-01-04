using Api.Models;
using Api.Models.Enums;
using Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Api.Authorization;

public class CanManageVehicleRequirement : IAuthorizationRequirement
{
}

public class CanManageVehicleHandler : AuthorizationHandler<CanManageVehicleRequirement, Vehicle>
{
    
    private readonly IPermissionService _permissionService;

    public CanManageVehicleHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanManageVehicleRequirement requirement,
        Vehicle vehicle)
    {
        if (_permissionService.SiteRole == SiteRole.Admin ||
            vehicle.UserOwnerId == _permissionService.UserId ||
            (vehicle.TeamOwnerId == _permissionService.CompanyId && _permissionService.TeamRole == TeamRole.Admin))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;

    }
}