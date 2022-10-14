using EcommerceProject.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendasService _graficoVendas;

        public AdminGraficoController(GraficoVendasService graficoVendas)
        {
            _graficoVendas = graficoVendas ?? throw new ArgumentNullException(nameof(graficoVendas));
        }

        public JsonResult VendasShirts(int dias)
        {
            var shirtsVendasTotais = _graficoVendas.GetVendasShirts(dias);
            return Json(shirtsVendasTotais);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult VendasMensal()
        {
            return View();
        }


        [HttpGet]
        public IActionResult VendasSemanal()
        {
            return View();
        }
    }
}
