using Microsoft.EntityFrameworkCore;
using OrderRabbitMQApi.Models;

namespace OrderRabbitMQApi.Data.Context;

public class SQLContext : DbContext
{
    public SQLContext(DbContextOptions<SQLContext> options) : base(options) { }

    public SQLContext() { }

    public DbSet<Order> orders { get; set; }      
}
