using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderRabbitMQApi.DTO;
using OrderRabbitMQApi.Repository;

namespace OrderRabbitMQApi.Controller
{
    [Route("api/v1[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CellConcertOrderDTO>>> GetAll()
        {
            var odr = await _orderRepository.GetAll();
            return Ok(odr);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CellConcertOrderDTO>> GetById(int id)
        {
            var msg = await _orderRepository.GetByIdAsync(id);

            if (msg.Id <= 0) return NotFound("Ordem de serviço " + id + " não foi encontrada !!!");
            return Ok(msg);
        }

         /// <summary>
         ///  Essa Api Não possui o POST, pois o POST é através de um Consumer(Consumidor - RabbitMQ
         ///  que é lancada por outra API somente pelo Postman - Tudo Isso para fins didáticos, ou seja,
         ///  essa API faz parte de uma "triade" de Producer - Consumer e ESTA para manipular ou deletar.
         /// </summary>
         /// 

        [HttpPut("From-Body")]
        public async Task<ActionResult<CellConcertOrderDTO>> Updade([FromBody] CellConcertOrderDTO dto)
        {
            try
            {                     
                var cel = await _orderRepository.Update(dto);
                return Ok(cel);
            }
            catch (Exception)
            {

                return BadRequest("Ordem não encontrada !");
            }              
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var status = await _orderRepository.Delete(id);
            // Se o status for false -> " if (!status)  "
            if (!status) return BadRequest("Ordem de concerto " + id + " não foi encontrada !!!");
            return Ok(status);
        }
    }
}
