using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using API.Validators;
using API.Validators.Interfaces;
using DataLibrary.DataAccess;
using DataLibrary.DataAccess.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
const string CORS_NAME = "corspolicy";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataAccess, SqlDataAccess>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesValidator, CategoriesValidator>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddScoped<IInvoicesRepository, InvoicesRepository>();
builder.Services.AddScoped<IInvoicesValidator, InvoicesValidator>();
builder.Services.AddScoped<IInvoicesService, InvoicesService>();

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersValidator, OrdersValidator>();
builder.Services.AddScoped<IOrdersService, OrdersService>();


builder.Services.AddScoped<IOrdersProductsRepository, OrdersProductsRepository>();
builder.Services.AddScoped<IOrdersProductsValidator, OrdersProductsValidator>();
builder.Services.AddScoped<IOrdersProductsService, OrdersProductsService>();

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsValidator, ProductsValidator>();
builder.Services.AddScoped<IProductsService, ProductsService>();

builder.Services.AddScoped<IProductsCategoriesRepository, ProductsCategoriesRepository>();
builder.Services.AddScoped<IProductsCategoriesValidator, ProductsCategoriesValidator>();
builder.Services.AddScoped<IProductsCategoriesService, ProductsCategoriesService>();

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: CORS_NAME,
      policy =>
      {
          policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod();
      });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "ClientApp/dist")),
    RequestPath = "/ClientApp/dist"
});

app.UseForwardedHeaders();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(CORS_NAME);

app.MapControllers();

app.Run();
