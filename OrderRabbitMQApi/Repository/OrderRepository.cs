using Microsoft.EntityFrameworkCore;
using OrderRabbitMQApi.Data.Context;
using OrderRabbitMQApi.Models;

namespace OrderRabbitMQApi.Repository;

public class OrderRepository : IOrderRepository
{

    private readonly SQLContext _context;
    
    public OrderRepository(SQLContext context)
    {
        _context = context;
    }

    public async Task<Order[]> GetAll()
    {
        IQueryable<Order> query = _context.orders;

        query = query.AsNoTracking()
                     .OrderBy(o => o.Id);

        return (await query.ToArrayAsync());
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        Order cel = await _context.orders
       .Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Order();
        return await _context.orders.FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    ///  Essa Api Não possui o POST, pois o POST é atravez de um Producer que manda os dados para fila do
    ///  RabbitMQ atravéz do "Postman", que por sua vez é consumido e persistido no Banco de Dados por outra
    ///  API que é um Consumer, e, esse consumer faz pesistir os dados no Banco de Dados.
    ///  Essa API aqui que deveria ter um "POST" faz parte de uma "triade" de Producer - Consumer e ESTA API é
    ///  somente para visualizar atualizar ou deletar dados.
    /// </summary>

    public void Update<T>(T entity) where T : class
    {
         _context.Update(entity);
    }
    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0; // Se > 0 Retorna True.
    }
}