global using Microsoft.EntityFrameworkCore;
global using IcePayment.API.Data;
using IcePayment.API.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseInMemoryDatabase("PaymentDB");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
