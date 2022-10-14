using EcommerceProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EcommerceProject.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize("Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostEnvironment, IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnvironment = hostEnvironment;
            _myConfig = myConfiguration.Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if ( files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Erro : Arquivo não selecionado";
                return View(ViewData);
            }
            if (files.Count > 10)
            {
                ViewData["Erro"] = "Erro : Excedeu o limite de arquivos (Máx:10)";
                return View(ViewData);
            }

            long size = files.Sum(f=> f.Length);

            var filePathsName = new List<string>();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            foreach (var formFile in files)
            {
                if(formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".png") || formFile.FileName.Contains(".gif"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} files enviados para o servidor " + $"com tamanho total de {size} bytes";
            ViewBag.Arquivos = filePathsName;
            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);
            FileInfo[] files = dir.GetFiles();

            model.PathImagensProduto = _myConfig.NomePastaImagensProdutos;

            if(files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files=files;

            return View(model);

        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,_myConfig.NomePastaImagensProdutos + "\\", fname);

            if (System.IO.File.Exists(_imagemDeleta))
            {
                System.IO.File.Delete(_imagemDeleta);
                ViewData["Deletado"] = $"Arquivos {_imagemDeleta} apagado com sucesso";
            }
            return View("index");
        }
    }
}
