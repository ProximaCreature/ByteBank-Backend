using ByteBank.API.BillDiscount;
using ByteBank.API.BillDiscount.Application.Features;
using ByteBank.API.BillDiscount.Domain.Models.ValueObjects;
using Microsoft.OpenApi.Models;
using ByteBank.API.Security;
using ByteBank.API.Security.Interfaces.REST.Middleware;
using ByteBank.API.Shared;
using ByteBank.API.Shared.Infrastructure.Persistence.Configuration;
using ByteBank.API.Shared.Interfaces.REST.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddSharedService(builder.Configuration);
builder.Services.AddSecurityServices();
builder.Services.AddBillDiscountService();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
await using (var context = scope.ServiceProvider.GetService<ServiceDbContext>())
{
    context!.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllers();
// link: https://www.e-financebook.com/aplicaciones/e-Book4/Anexos/Cap%2005%20-%20Tasa%20Descontada%20-%20Solucionario%2037.pdf
// var faceValue = 65850;
// var tep = FinancialOperation.ConvertToNewTEPFromTEP(0.24, Period.ANUAL, 120);
// Console.WriteLine(tep);
// var netValue = FinancialOperation.CalculateNetValue(faceValue, tep);
// Console.WriteLine(netValue);
// var receivedValue = FinancialOperation.CalculateReceivedValue(faceValue, netValue, 170, 0.003, 0.1);
// Console.WriteLine(receivedValue);
// var deliveredValue = FinancialOperation.CalculateDeliveredValue(faceValue, 27, 0.1);
// Console.WriteLine(deliveredValue);
// var tcea = FinancialOperation.CalculateTCEA(receivedValue, deliveredValue, 120);
// Console.WriteLine(tcea);
// var iComp = FinancialOperation.CalculateCompensatoryInterest(faceValue, 0.24, Period.ANUAL, 6);
// Console.WriteLine(iComp);
// var tepMora = FinancialOperation.ConvertToNewTEPFromTN(0.12, Period.ANUAL, 360);
// var iMora = FinancialOperation.CalculateMoratoriumInterest(faceValue, tepMora, Period.ANUAL, 6);
// Console.WriteLine(iMora);

app.Run();
