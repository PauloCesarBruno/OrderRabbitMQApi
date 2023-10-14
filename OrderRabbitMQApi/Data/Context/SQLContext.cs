using Microsoft.EntityFrameworkCore;
using OrderRabbitMQApi.Models;

namespace OrderRabbitMQApi.Data.Context;

public class SQLContext : DbContext
{
    public SQLContext(DbContextOptions<SQLContext> options) : base(options) { }

    public SQLContext() { }

    public DbSet<CellConcertOrder> orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CellConcertOrder>()
            .Property(vc => vc.ValorConserto)
            .HasColumnType("decimal(18, 2)");
    }
}
