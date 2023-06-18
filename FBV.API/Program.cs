using FBV.API.Managers;
using FBV.DAL.Contracts;
using FBV.DAL.Data;
using FBV.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FBVContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("FBVConnection")));
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();

builder.Services.AddTransient<IFileWrapper, FileWrapper>();

builder.Services.AddTransient<IPurchaseOrderProcessor, PurchaseOrderProcessor>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
