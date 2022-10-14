using EcommerceProject.Models;
using EcommerceProject.Repositories.Interfaces;
using EcommerceProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShirtRepository _shirtRepository;

        public HomeController(IShirtRepository shirtRepository)
        {
            _shirtRepository = shirtRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                FavouriteShirts = _shirtRepository.FavouriteShirts

            };
            return View(homeViewModel);
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}