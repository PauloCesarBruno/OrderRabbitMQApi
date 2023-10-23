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

        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            List<Order> cel = await _context.orders.ToListAsync();
            return _mapper.Map<List<OrderDTO>>(cel);
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            Order cel = await _context.orders
           .Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Order();
            return _mapper.Map<OrderDTO>(cel);
        }

        public async Task<OrderDTO> Update(OrderDTO dto)
        {
            Order cel = _mapper.Map<Order>(dto);
            _context.orders.Update(cel);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(cel);
        }
        public async Task<bool> Delete(int id)
        {

            try
            {
                Order cel = await _context.orders
                .Where(o => o.Id == id).FirstOrDefaultAsync() ?? new Order();
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