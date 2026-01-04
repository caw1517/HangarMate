using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Api.Authorization;

public class SelfOrAdminRequirement : IAuthorizationRequirement
{
    
}

public class SelfOrAdminHandler : AuthorizationHandler<SelfOrAdminRequirement, Guid>
{

    private readonly IPermissionService _permissionService;

    public SelfOrAdminHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SelfOrAdminRequirement requirement, Guid targetUserId)
    {
        if (_permissionService.SiteRole == SiteRole.Admin || _permissionService.UserId == targetUserId)
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}