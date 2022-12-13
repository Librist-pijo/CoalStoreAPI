using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using DataLibrary.DataAccess;
using DataLibrary.DataAccess.Interfaces;
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
builder.Services.AddScoped<ICategoriesRepository, CategoriesRespository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IInvoicesRepository, InvoicesRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersProducts, OrdersProductsRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsCategories, ProductsCategoriesRepository>();

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: CORS_NAME,
      policy =>
      {
          policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod();
      });
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(CORS_NAME);

app.MapControllers();

app.Run();
