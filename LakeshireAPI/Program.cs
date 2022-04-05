using System.Text;
using Lakeshire.Common.Configs;
using Lakeshire.Common.DAL.DataContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var authSettings = builder.Configuration.GetSection("JwtAuth").Get<JwtAuthConfig>();
if (authSettings == default)
    throw new Exception("Failed to find the JwtAuth section in appsettings.json!");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = authSettings.Audience,
            ValidIssuer = authSettings.Issuer,
            ValidateIssuer = authSettings.ValidateIssuer,
            ValidateAudience = authSettings.ValidateAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.PrivateKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(60)
        };
    });

builder.Services.AddOptions();
builder.Services.Configure<JwtAuthConfig>(builder.Configuration.GetSection("JwtAuth"));

var entityContextConStr = builder.Configuration.GetConnectionString("EntityContext");
if (entityContextConStr == default)
    throw new Exception("Failed to find the EntityContext connection string in appsettings.json!");
builder.Services.AddDbContext<EntityContext>(options => options.UseSqlServer(entityContextConStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register endpoints here
app.MapGet("/", httpContext =>
{
    httpContext.Response.Redirect("/swagger"); 
    return Task.CompletedTask;
});

app.Run();