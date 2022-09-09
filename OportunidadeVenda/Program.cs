using Microsoft.EntityFrameworkCore;
using OportunidadeVenda.Context;
using OportunidadeVenda.Data.CnpjApiData.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICnpjApiService, CnpjApiService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer
    ("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=vendasdb;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

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
