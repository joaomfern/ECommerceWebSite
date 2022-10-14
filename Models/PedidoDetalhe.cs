using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.Models
{
    public class PedidoDetalhe
    {
        public int PedidoDetalheId { get; set; }
        public int PedidoId { get; set; }
        public int ShirtId { get; set; }
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        //relacao de muitos para um (um pedido tem varios pedidos detalhes, pedidos detalhe contem varias shirts)
        public virtual Shirt Shirt { get; set; }
        public virtual Pedido Pedido { get; set; }

    }
}
