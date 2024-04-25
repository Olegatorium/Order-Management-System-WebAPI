using DataBaseContent;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add services into IoC container
builder.Services.AddScoped<IOrdersService, OrderService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddTransient<IOrderTotalCalculator, OrderTotalCalculator>();


builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

