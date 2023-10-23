using AutoMapper;
using OrderRabbitMQApi.DTO;
using OrderRabbitMQApi.Models;

namespace OrderRabbitMQApi.MapConfig;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var MappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<OrderDTO, Order>().ReverseMap();

        });
        return MappingConfig;
    }
}
