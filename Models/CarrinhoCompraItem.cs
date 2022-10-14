using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.Models
{
    [Table("CarrinhoCompraItens")]
    public class CarrinhoCompraItem
    {
        public int CarrinhoCompraItemId { get; set; }
        public Shirt Shirt { get; set; }
        public int Quantidade { get; set; }

        [StringLength(200)]
        public string CarrinhoCompraID { get; set; }

    }
}
