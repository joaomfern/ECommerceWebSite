using EcommerceProject.Models;
using EcommerceProject.Repositories.Interfaces;
using EcommerceProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class ShirtController : Controller
    {
        public ShirtController(IShirtRepository shirtRepository, ICategoriaRepository categoriaRepository)
        {
            _shirtRepository = shirtRepository;
            _categoriaRepository = categoriaRepository;
        }

        private readonly IShirtRepository _shirtRepository;
        private readonly ICategoriaRepository _categoriaRepository;

      
        public IActionResult List( string categoria)
        {

            //Retorna todas as camisolas por categoria usando ViewModels
            IEnumerable<Shirt> shirts;
            string categoriaAtual = string.Empty;

            //Se nao for passada nenhuma categoria, retorna todos as camisolas, se sim filtra por categoria
            if (string.IsNullOrEmpty(categoria))
            {
                shirts = _shirtRepository.Shirts.OrderBy(l => l.ShirtId);
                categoriaAtual = "Todas as camisolas";
            }
            else
            {

                shirts = _shirtRepository.Shirts.Where(l => l.Categoria.CategoriaNome.Equals(categoria)).OrderBy(l => l.Nome);
                categoriaAtual = categoria;

            }

            var shirtsListViewModel = new ShirtListViewModel
            {
                Shirts = shirts,
                CategoriaAtual = categoriaAtual
            };

            ViewBag.Total = "Total de camisolas : ";
            var totalshirts = shirts.Count();
            ViewBag.TotalShirts = totalshirts;
            return View(shirtsListViewModel);



            //Retorna todas as camisolas de todas as categorias usando Model da classe de dominio

            //ViewData["Titulo"] = "Todas as camisolas";
            //var shirts = _shirtRepository.Shirts;
            //var totalshirts = shirts.Count();
            //ViewBag.Total = "Total de camisolas : ";
            //ViewBag.TotalShirts = totalshirts;
            //return View(shirts);



            //Retorna todas as camisolas de todas as categorias usando ViewModels

            //var shirtListViewModel = new ShirtListViewModel();
            //shirtListViewModel.Shirts = _shirtRepository.Shirts;
            //shirtListViewModel.CategoriaAtual = "Categoria Atual";
            //ViewData["Titulo"] = shirtListViewModel.CategoriaAtual;

            //return View(shirtListViewModel);

        }

        public IActionResult Details(int shirtId)
        {
            var shirt = _shirtRepository.Shirts.FirstOrDefault(l => l.ShirtId == shirtId);
            return View(shirt);
        }

        public ViewResult Search (string searchString)
        {
            IEnumerable<Shirt> shirts;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                shirts = _shirtRepository.Shirts.OrderBy(p=>p.ShirtId);
                categoriaAtual = "Todas as camisolas";

            }
            else
            {
                shirts = _shirtRepository.Shirts.Where(p=>p.Nome.ToLower().Contains(searchString.ToLower()));

                if (shirts.Any())
                {
                    categoriaAtual = "Camisolas";
                }
                else
                {
                    categoriaAtual = "Nenhum produto foi encontrado";
                }
         
            }

            return View("~/Views/Shirt/List.cshtml", new ShirtListViewModel
            {
                Shirts = shirts,
                CategoriaAtual=categoriaAtual
            });

        }
    }
}
