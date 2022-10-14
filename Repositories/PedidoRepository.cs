using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interfaces;

namespace EcommerceProject.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido); //adiciona o pedido a tabela pedidos
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems; //vai adicionar o carrinho de compra itens 

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    ShirtId = carrinhoItem.Shirt.ShirtId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Shirt.Preco
                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetail); //cria na tabela pedido detalhes
            }

            _appDbContext.SaveChanges();
        }
    }
}
