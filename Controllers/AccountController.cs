using EcommerceProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet] //página que fornece o layout de login
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM) //metodo que permite ao user postar 
        {
            // se nao estiver válido retorna a pagina inicial
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            //se estiver procura o nome na tabela de users
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            
            //se existir na tabela
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                
                // se foi feito com sucesso, retorna para a pagina dos logados
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            //se nao existir dá mensagem de erro
            ModelState.AddModelError("", "Falha ao realizar o login!");
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //previne que hackers tenham acesso aos privilegios de utilizadores logados
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                //cria o usuario
                var user = new IdentityUser { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user,registroVM.Password);

                // se cria com sucesso, atribui o perfil de member ao user e reencaminha para página de login
                if (result.Succeeded)
                {                    
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    //codigo que identifica os erros no registro
                    List<IdentityError> errorList = result.Errors.ToList();
                    var errors = string.Join(", ", errorList.Select(e => e.Description));

                    this.ModelState.AddModelError("Registro", "Falha ao resgistrar o usuário - " + errors);
                }
            }
            return View(registroVM);
        }

        [HttpPost]
        //código que permite fazer o logout ao utilizador e limpa todos os dados da sessão
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
