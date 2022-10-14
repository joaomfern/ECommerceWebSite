using EcommerceProject.Models;
using EcommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class PedidoController : Controller
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet] //quando é get o VS assume sempre por default
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost] //quando é post tem sempre de se indicar,
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //obtem os itens do carrinho de compra do cliente

            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems=items;

            //verificar se existem itens do pedido

            if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Seu Carrinho esta vazio, adicione uma camisola");
            }

            //calcula o total de itens e o total do pedido
            foreach( var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Shirt.Preco * item.Quantidade);
            }

            //atribuir os valores obtidos ao pedido

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //valida os dados do pedido

            if (ModelState.IsValid)
            {
                //cria o pedido e o pedidodetail

                _pedidoRepository.CriarPedido(pedido);

                //define mensagens ao cliente
                ViewBag.CheckOutCompletoMensagem = "Obrigado pelo seu pedido";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //limpa o carrinho do cliente
                _carrinhoCompra.LimparCarrinho();

                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml",pedido);
            }

            return View(pedido);
        }
    }
}
