using EcommerceProject.Models;

namespace EcommerceProject.ViewModels
{
    public class ShirtListViewModel
    {
       public IEnumerable<Shirt> Shirts { get; set; }
       public string CategoriaAtual { get; set; }

    }
}
