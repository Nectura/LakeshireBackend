using System.Text;
using Lakeshire.Common.Configs;
using Lakeshire.Common.DAL.DataContexts;
using Lakeshire.Common.DAL.WorkUnits;
using Lakeshire.Common.DAL.WorkUnits.Interfaces;
using Lakeshire.Common.Services;
using LakeshireAuth.Endpoints;
using LakeshireAuth.Services;
using LakeshireAuth.Services.Interfaces;
using LakeshireAuth.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddValidators<IValidator, IAsyncValidator<IAsyncValidatorRequest, IAsyncValidatorResponse>>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MainPolicy",
        pBuilder =>
        {
            pBuilder.WithOrigins("https://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("X-User-Agent", "X-Grpc-Web", "content-type", "x-grpc-web", "x-user-agent", "grpc-status", "grpc-message", "authorization");
        });
});

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

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtAuthService>();
builder.Services.AddScoped<RegexValidationService>();

builder.Services.AddScoped<PasswordStructureValidator>();
builder.Services.AddScoped<PasswordValidator>();
builder.Services.AddScoped<LoginValidator>();
builder.Services.AddScoped<RegistrationValidator>();
builder.Services.AddScoped<RefreshTokenValidator>();

// Add the units of work here
builder.Services.AddScoped<IAuthWorkUnit, AuthWorkUnit>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MainPolicy");

// Register endpoints here
app.MapGet("/", httpContext =>
{
    httpContext.Response.Redirect("/swagger"); 
    return Task.CompletedTask;
});
app.AddLoginEP();
app.AddRegisterEP();
app.AddRefreshTokenEP();

app.Run();