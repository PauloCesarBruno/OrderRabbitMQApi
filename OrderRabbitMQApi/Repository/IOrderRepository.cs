using OrderRabbitMQApi.DTO;

namespace OrderRabbitMQApi.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAll();
        Task<OrderDTO> GetByIdAsync(int Id);
        Task<OrderDTO> Update(OrderDTO dto);
        Task<bool> Delete(int id);

    }
}
