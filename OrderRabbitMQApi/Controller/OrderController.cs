using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderRabbitMQApi.DTO;
using OrderRabbitMQApi.Models;
using OrderRabbitMQApi.Repository;
//

namespace OrderRabbitMQApi.Controller;

[Route("api/v1/[controller]")]
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
    public async Task<ActionResult<IEnumerable<Order>>> GetAll()
    {
        var odr = await _orderRepository.GetAll();
        return Ok(_mapper.Map<IEnumerable<OrderDTO>>(odr));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(int id)
    {
        var odr = await _orderRepository.GetByIdAsync(id);

        if (odr == null)
        {
            return BadRequest("Order " + id + " não foi localizada !");
        }

        var orderDto = _mapper.Map<OrderDTO>(odr);

        return Ok(orderDto);
    }

    /// <summary>
    ///  Essa Api Não possui o POST, pois o POST é atravez de um Producer que manda os dados para fila do
    ///  RabbitMQ atravéz do "Postman", que por sua vez é consumido e persistido no Banco de Dados por outra
    ///  API que é um Consumer, e, esse consumer faz pesistir os dados no Banco de Dados.
    ///  Essa API aqui que deveria ter um "POST" faz parte de uma "triade" de Producer - Consumer e ESTA API é
    ///  somente para visualizar atualizar ou deletar dados.
    /// </summary>

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, OrderDTO model)
    {
        try
        {
            var odr = await _orderRepository.GetByIdAsync(id);

            if (odr == null) BadRequest("Order não Encontrada !!!");

            //Abaixo Mapeamento para o PUT.
            _mapper.Map(model, odr);

            _orderRepository.Update(odr);

            if (await _orderRepository.SaveChangesAsync())
            {
                // Ao Ivés do OK (cód. 200, o Created retorna um 201).
                return Created($"/api/treino/{model.Id}", _mapper.Map<OrderDTO>(odr));
            }
        }
        catch (Exception)
        {
            // 
        }

        return BadRequest("Falha ao atualizar o  registro do Produto !!!");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Order>> Delete(int id)
    {
        var odr = await _orderRepository.GetByIdAsync(id);

        if (odr == null) return BadRequest("Order não Encontrada !!!");


        _orderRepository.Delete(odr);

        if (await _orderRepository.SaveChangesAsync())
        {
            return Ok("Produto Excluido com Sucesso !!!");
        }
        return BadRequest("Falha ao Excluir o registro do Produto !!!");
    }
}
