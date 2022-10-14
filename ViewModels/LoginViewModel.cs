using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Informe o Nome")]
        [Display(Name ="Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a passsword")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}
