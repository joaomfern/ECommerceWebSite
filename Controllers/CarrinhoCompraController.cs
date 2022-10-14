using EcommerceProject.Models;
using EcommerceProject.Repositories.Interfaces;
using EcommerceProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly IShirtRepository _shirtRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(IShirtRepository shirtRepository, CarrinhoCompra carrinhoCompra)
        {
            _shirtRepository = shirtRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();

            _carrinhoCompra.CarrinhoCompraItems=itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        [Authorize]
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra (int shirtId)
        {
            var shirtSelecionada = _shirtRepository.Shirts.FirstOrDefault(P=>P.ShirtId == shirtId);
            if(shirtSelecionada != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(shirtSelecionada);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra (int shirtId)
        {
            var shirtSelecionada = _shirtRepository.Shirts.FirstOrDefault(p=>p.ShirtId==shirtId);
            if(shirtSelecionada != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(shirtSelecionada);

            }
            return RedirectToAction("Index");
        }
    }
}
