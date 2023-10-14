using System.ComponentModel.DataAnnotations;

namespace OrderRabbitMQApi.DTO
{
    public class CellConcertOrderDTO
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }
        public string MarcaAparelho { get; set; }
        public string ModeloAparelho { get; set; }
        public bool Reparado { get; set; }
        public decimal ValorConserto { get; set; }
    }
}
