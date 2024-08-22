
using Microsoft.EntityFrameworkCore;
using SQLiteApplication.Entity;
using SQLiteApplication.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IProductRepository, ProductRepository>(); 

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connectionString = "Data Source=localhost;Initial Catalog=YOUR_DATABASE;User ID=sa;Password=********;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

    options.UseSqlServer(connectionString);

});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }