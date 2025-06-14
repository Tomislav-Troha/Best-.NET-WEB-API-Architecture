using BestArchitecture.Api.DependencyInjection;
using BestArchitecture.Api.Middlewares;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Registracija slojeva
builder.Services.AddInfrastructure(builder.Configuration);

// Registriraj controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    options.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

    // JWT token support
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert token (without Bearer)."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Ostalo
Env.Load();

var jwtKey = Environment.GetEnvironmentVariable("JwtKey");
var jwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer");

if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 10)
    throw new ArgumentException("JWT key is not set correctly!");

if (string.IsNullOrEmpty(jwtIssuer))
    throw new ArgumentException("JWT issuer not set!!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey)),

        ValidateAudience = true,
        ValidAudiences =
        [
            "my-app",
        ],

        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization();

builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();
builder.Services.AddCors(o => o.AddPolicy("AllowAllOrigins", p =>
                                          p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Config
var config = builder.Configuration;

config.GetConnectionString("DefaultConnection")
    ?.Replace("__DefaultConnectionPassword__", Environment.GetEnvironmentVariable("DefaultConnectionPassword"));

config.GetConnectionString("MyOracleSQLConnection")
    ?.Replace("__OraclePassword__", Environment.GetEnvironmentVariable("OraclePassword"));

var app = builder.Build();

// Middleware

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

await app.RunAsync();