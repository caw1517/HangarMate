using System.Security.Claims;
using Api.Authorization;
using Api.Context;
using Api.Models;
using Api.Models.Enums;
using Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var supabaseUrl = builder.Configuration["Authentication:Supabase:Url"];
var supabaseProjectId = builder.Configuration["Authentication:Supabase:ProjectId"];

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

/*SERVICES*/
builder.Services.AddScoped<LogItemService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

/*POLICIES*/
builder.Services.AddScoped<IAuthorizationHandler, SelfOrAdminHandler>();
builder.Services.AddScoped<IAuthorizationHandler, CanManageVehicleHandler>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{supabaseUrl}/auth/v1";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"{supabaseUrl}/auth/v1",
            ValidateAudience = true,
            ValidAudience = "authenticated",
            ValidateLifetime = true
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var dbContext = context.HttpContext.RequestServices.GetRequiredService<DatabaseContext>();

                var authId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(authId, out var userId))
                {
                    var profile = await dbContext.UserProfiles
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == userId);

                    if (profile != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("CompanyId", profile.TeamId.ToString() ?? ""),
                            new Claim("TeamRole", profile.TeamRole.ToString() ?? "None"),
                            new Claim("SiteRole", profile.SiteRole.ToString()),
                            new Claim("LicenseType", profile.LicenseType.ToString())
                        };

                        var appIdentity = new ClaimsIdentity(claims);
                        context.Principal?.AddIdentity(appIdentity);
                    }
                }
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SiteAdmin", policy => policy.RequireClaim("SiteRole", SiteRole.Admin.ToString()));
    options.AddPolicy("TeamAdmin", policy => policy.RequireClaim("TeamRole", TeamRole.Admin.ToString()));
    options.AddPolicy("CanManageUsers", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("SiteRole", SiteRole.Admin.ToString()) ||
            context.User.HasClaim("TeamRole", TeamRole.Admin.ToString())
        ));
    
    //User Resource based policies
    options.AddPolicy("SelfOrAdmin", policy => policy.Requirements.Add(new SelfOrAdminRequirement()));
    
    //Vehicle Resource based polices
    options.AddPolicy("CanManageVehicle", policy => policy.Requirements.Add(new CanManageVehicleRequirement()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();