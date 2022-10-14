using EcommerceProject.Context;
using EcommerceProject.Models;

namespace EcommerceProject.Areas.Admin.Servicos
{
    public class GraficoVendasService
    {
        private readonly AppDbContext context;

        public GraficoVendasService(AppDbContext context)
        {
            this.context = context;
        }

        public List<ShirtGrafico> GetVendasShirts(int dias = 360)
        {
            var data = DateTime.Now.AddDays(-dias);

            var shirts = (from pd in context.PedidoDetalhes
                          join l in context.Shirts on pd.ShirtId equals l.ShirtId
                          where pd.Pedido.PedidoEnviado >= data
                          group pd by new { pd.ShirtId, l.Nome } 
                          into g select new
                          {
                              ShirtNome = g.Key.Nome,
                              ShirtsQuantidade = g.Sum(q => q.Quantidade),
                              ShirtsValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                          });

            var lista = new List<ShirtGrafico>();

                foreach(var item in shirts)
            {
                var shirt = new ShirtGrafico();
                shirt.ShirtNome = item.ShirtNome;
                shirt.ShirtsQuantidade = item.ShirtsQuantidade;
                shirt.ShirtsValorTotal = item.ShirtsValorTotal;
                lista.Add(shirt);

            }

            return lista;

        }

    }
}
