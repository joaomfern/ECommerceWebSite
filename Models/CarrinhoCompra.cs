using EcommerceProject.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string  CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão ( se true apanha a sessao existente se false cria uma nova)
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um servico do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //se true obtem o id do carrinho se false gera o Id do Carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //atribui o ID do carrinho na Session
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };

        }

        public void AdicionarAoCarrinho(Shirt shirt)
        {
            //verificar se existe a camisola selecionada no carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault
                (s => s.Shirt.ShirtId == shirt.ShirtId && s.CarrinhoCompraID == CarrinhoCompraId);

            //se nao existir uma camisola, adiciona uma camisola
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraID = CarrinhoCompraId,
                    Shirt = shirt,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            //se existir a camisola adiciona uma quantidade
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            //grava as alteraçoes
            _context.SaveChanges();
        }

        public void RemoverDoCarrinho (Shirt shirt)
        {
            //verificar se existe a camisola selecionada no carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s=> s.Shirt.ShirtId == shirt.ShirtId && s.CarrinhoCompraID==CarrinhoCompraId);

            //var quantidadeLocal = 0;
           
            if (carrinhoCompraItem != null)
            {
                //se existir e for maior que 1 reduz 1 quantidade
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    //quantidadeLocal = carrinhoCompraItem.Quantidade;

                }
                //se existir e for igual 1 elimina o item do carrinho
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            //return quantidadeLocal;
        }  

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            // se nao for null retorna CarrinhoCompraItems, se for null cria uma nova instancia
            return CarrinhoCompraItems ??
                (CarrinhoCompraItems = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraID == CarrinhoCompraId)
                .Include(s => s.Shirt)
                .ToList());
        }

        public void LimparCarrinho()
        {
            //verificar os items que pertencem a este carrinho e remover todos
            var carrinhoItens = _context.CarrinhoCompraItens.Where(carrinho => carrinho.CarrinhoCompraID == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();

        }

        public decimal GetCarrinhoCompraTotal()
        {   
            //verificar os items que pertencem a este carrinho e somar todos
            var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraID == CarrinhoCompraId).Select(c=>c.Shirt.Preco * c.Quantidade).Sum();
            return total;
        }
    }
}
