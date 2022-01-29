global using Microsoft.EntityFrameworkCore;
global using IcePayment.API.Data;
using IcePayment.API.Data.Repositories;
using IcePayment.API.Helpers;
using IcePayment.API.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<PaymentValidator>();
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseInMemoryDatabase("PaymentDB");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

// for integration tests
public partial class Program { }