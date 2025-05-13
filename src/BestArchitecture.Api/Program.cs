using BestArchitecture.Api.Collections;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1) Registracija slojeva
builder.Services.AddInfrastructure(builder.Configuration);

// 2) Registriraj controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Best Architecture API", Version = "v1" });
    var xmlFile = Path.ChangeExtension(typeof(Program).Assembly.Location, ".xml");
    // c.IncludeXmlComments(xmlFile);
});

// 3) Ostalo
builder.Services.AddHealthChecks();
builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();
builder.Services.AddCors(o => o.AddPolicy("AllowAllOrigins", p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// 4) Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyShop API V1");
    c.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
