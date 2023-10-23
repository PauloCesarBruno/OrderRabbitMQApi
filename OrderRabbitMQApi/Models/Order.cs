namespace OrderRabbitMQApi.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime DataRegistro { get; set; }          
    public string Nome { get; set; }
    public string CPF { get; set; }          
    public string Email { get; set; }         
    public string Telefone { get; set; }        
    public string Cartao { get; set; }
    public string NumeroCartao { get; set; }
    public string DataVencimento { get; set; }
    public string CVV { get; set; }
}
