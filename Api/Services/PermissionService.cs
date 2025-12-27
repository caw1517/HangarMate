using System.Security.Claims;
using Api.Context;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public interface IPermissionService
{
    Guid UserId { get; }
    int? CompanyId { get; }
    bool IsInCompany { get; }
    
    CompanyRole CompanyRole { get; }
    SiteRole SiteRole { get; }
    LicenseType LicenseType { get; } 
    
    /*bool IsSiteAdmin { get; }
    bool IsCompanyAdmin { get; }*/
}

public class PermissionService : IPermissionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

    public int? CompanyId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst("CompanyId")?.Value;
            return string.IsNullOrEmpty(claim) ? null : int.Parse(claim);
        }
    }
    
    public bool IsInCompany => CompanyId.HasValue;
    
    public SiteRole SiteRole => Enum.Parse<SiteRole>(_httpContextAccessor.HttpContext?.User.FindFirst("SiteRole")?.Value ?? "Standard");
    public CompanyRole CompanyRole => Enum.Parse<CompanyRole>(_httpContextAccessor.HttpContext?.User.FindFirst("CompanyRole")?.Value ?? "Standard");
    public LicenseType LicenseType => Enum.Parse<LicenseType>(_httpContextAccessor.HttpContext?.User.FindFirst("LicenseType")?.Value ?? "None");
}