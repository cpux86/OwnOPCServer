
using System.Runtime.CompilerServices;
using Application;
using Application.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.IdentityModel.Tokens;
using WebApi.Middleware;


var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService()
        ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(options);
builder.Configuration.AddJsonFile("config.json",true,true);
//builder.Configuration.AddYamlFile("config.yml", true, true);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    }
);

//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;

//    })
//    .AddIdentityServerAuthentication(options =>
//    {
//        options.ApiName = "ServiceApi";
//        options.Authority = "https://localhost:5501";
//        options.RequireHttpsMetadata = false;


//    });




builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5501";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateLifetime = true,
            //ClockSkew = TimeSpan.FromSeconds(60)
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Host.UseWindowsService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomExceptionHandler();
//app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
