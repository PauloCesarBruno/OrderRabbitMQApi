using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderRabbitMQApi.Data.Context;
using OrderRabbitMQApi.DTO;
using OrderRabbitMQApi.Models;

namespace OrderRabbitMQApi.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly SQLContext _context;
        private IMapper _mapper;

        public OrderRepository(SQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }             

        public async Task<IEnumerable<CellConcertOrderDTO>> GetAll()
        {
            List<CellConcertOrder> cel = await _context.orders.ToListAsync();
            return _mapper.Map<List<CellConcertOrderDTO>>(cel);
        }

        public async Task<CellConcertOrderDTO> GetByIdAsync(int id)
        {
            CellConcertOrder cel = await _context.orders
           .Where(p => p.Id == id).FirstOrDefaultAsync() ?? new CellConcertOrder();
            return _mapper.Map<CellConcertOrderDTO>(cel);
        }

        public async Task<CellConcertOrderDTO> Update(CellConcertOrderDTO dto)
        {
            CellConcertOrder cel = _mapper.Map<CellConcertOrder>(dto);
            _context.orders.Update(cel);
            await _context.SaveChangesAsync();
            return _mapper.Map<CellConcertOrderDTO>(cel);
        }
        public async Task<bool> Delete(int id)
        {

            try
            {
                CellConcertOrder cel = await _context.orders
                .Where(o => o.Id == id).FirstOrDefaultAsync() ?? new CellConcertOrder();
                if (cel.Id <= 0) return false;
                _context.orders.Remove(cel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}