using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Payment_API.src.Models.Interfaces;
using Payment_API.src.Persistence;
using Payment_API.src.Persistence.Repository;
using Payment_API.src.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaymentContext>(options => options.UseInMemoryDatabase("PaymentDb"));

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Payment API",
        Description = "Projeto ASP.NET Core Web API para registro de vendas."
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/api-docs/{documentName}/api-docs.json";
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/api-docs/v1/api-docs.json", "PAYMENT API V1");
        options.RoutePrefix = "api-docs";
        options.DocumentTitle = "Desafio Tech Pottencial";
        options.InjectStylesheet("/swagger-ui/custom.css");
    });

    app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
