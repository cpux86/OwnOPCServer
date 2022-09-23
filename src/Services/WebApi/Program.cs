
using Application;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using WebApi.Middleware;
var builder = WebApplication.CreateBuilder(args);
//Console.WriteLine(args[0]);
// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
