using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.Models
{
    [Table("Shirts")]
    public class Shirt
    {
        [Key]
        public int ShirtId { get; set; }

        [StringLength(80, MinimumLength = 10, ErrorMessage = "O tamanho máximo é de 80 caracteres e o minimo de 10.")]
        [Required(ErrorMessage = "Informe o nome da Camisola")]
        [Display(Name = "Nome da Camisola")]
        public string Nome { get; set;}

        [StringLength(200, MinimumLength = 20, ErrorMessage = "O tamanho máximo é de 200 caracteres e o minimo de 20.")]
        [Required(ErrorMessage = "Informe a descrição da Camisola")]
        [Display(Name = "Descrição da Camisola")]
        public string DescricaoCurta { get; set;}

        [StringLength(200, MinimumLength = 20, ErrorMessage = "O tamanho máximo é de 200 caracteres e o minimo de 20.")]
        [Required(ErrorMessage = "Informe a descrição da Camisola")]
        [Display(Name = "Descrição da Camisola")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço da Camisola")]
        [Display(Name = "Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99, ErrorMessage ="O preço deve estar entre 1 e 999.")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O tamanho máximo é de 200 caracteres.")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Mini")]
        [StringLength(200, ErrorMessage = "O tamanho máximo é de 200 caracteres.")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsFavourite { get; set; }

        [Display(Name = "Em Stock?")]
        public bool EmStock { get; set; }

        [Display(Name = "Categorias")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
