using OrderRabbitMQApi.DTO;

namespace OrderRabbitMQApi.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<CellConcertOrderDTO>> GetAll();
        Task<CellConcertOrderDTO> GetByIdAsync(int Id);
        Task<CellConcertOrderDTO> Update(CellConcertOrderDTO dto);
        Task<bool> Delete(int id);

    }
}
