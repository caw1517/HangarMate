using Api.Context;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<LogItemService>();
builder.Services.AddScoped<UsersService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtOptions =>
{
    
    //FIXME: add these values
    jwtOptions.Authority = builder.Configuration["AzureAd:Authority"];
    jwtOptions.Audience = builder.Configuration["AzureAd:ClientId"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();