using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderRabbitMQApi.Data.Context;
using OrderRabbitMQApi.MapConfig;
using OrderRabbitMQApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("ConectDB");
builder.Services.AddDbContext<SQLContext>(Options =>
                     Options.UseSqlServer(connection));


// Configurando Funcionamento do AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<IOrderRepository, OrderRepository>();


// Configurando e injetando Classes da Pasta Repository:
var build = new DbContextOptionsBuilder<SQLContext>();
build.UseSqlServer(connection);


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
