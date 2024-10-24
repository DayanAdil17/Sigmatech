using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sigmatech.Extensions;
using Sigmatech.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file if it exists
DotNetEnv.Env.Load();

// Access the configuration directly from the builder
var configuration = builder.Configuration;

// Add JWT Authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
    };
});

// Add database service with configuration
builder.Services.AddDatabase(configuration);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:3000",
                "http://unify.local",
                "http://unify.internal",
                "http://unify.local:3000",
                "http://unify.internal:3000")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddServices(configuration);

// Add repository to the container.
builder.Services.AddRepositories();

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddValidationHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins");

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

// Authentication & Authorization Middleware
app.UseAuthentication();  // Enable authentication middleware
app.UseAuthorization();   // Enable authorization middleware

app.MapControllers();

app.Run();
