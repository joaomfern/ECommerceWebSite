using EcommerceProject.Models;

namespace EcommerceProject.ViewModels
{
    public class PedidoShirtViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}
