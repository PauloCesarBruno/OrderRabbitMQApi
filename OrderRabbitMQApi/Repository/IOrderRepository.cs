using OrderRabbitMQApi.Models;

namespace OrderRabbitMQApi.Repository;

public interface IOrderRepository
{
    Task<Order[]> GetAll();
    Task<Order> GetByIdAsync(int Id);


    /// <summary>
    ///  Essa Api Não possui o POST, pois o POST é atravez de um Producer que manda os dados para fila do
    ///  RabbitMQ atravéz do "Postman", que por sua vez é consumido e persistido no Banco de Dados por outra
    ///  API que é um Consumer, e, esse consumer faz pesistir os dados no Banco de Dados.
    ///  Essa API aqui que deveria ter um "POST" faz parte de uma "triade" de Producer - Consumer e ESTA API é
    ///  somente para visualizar atualizar ou deletar dados.
    /// </summary>

    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;

    Task<bool> SaveChangesAsync();
}
