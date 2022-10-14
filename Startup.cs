using EcommerceProject.Areas.Admin.Servicos;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repositories;
using EcommerceProject.Repositories.Interfaces;
using EcommerceProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace EcommerceProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //servico que vai permitir fazer a ligação com a base de dados
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //servico que permite fazer a gestão de utilizadores
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //servico que associa a classe à pasta que definimos onde serão guardadas as fotos
            services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens"));
            

            //codigo que permite alterar as definicoes de password do identity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
              
            });

            //para utilizar repositorios
            services.AddTransient<IShirtRepository, ShirtRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository> ();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddScoped<RelatorioVendasService>();
            services.AddScoped<GraficoVendasService>();

            //para utilizar politicas de autorização nas páginas
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });
        
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

            //para utilizar Padrão MVC
            services.AddControllersWithViews();

            //para utilizar paginacao
            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
            });

            // para utilizar sessões
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //cria os perfis e cria os usuarios e atribui ao perfil
            seedUserRoleInitial.SeedRoles();
            seedUserRoleInitial.SeedUsers();

            //para permitir guardar sessoes
            app.UseSession();

            //para definir autorizacao e autenticação
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               
                //para conseguir utilzar áreas como admin
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                    );

                //para conseguir utilizar a barra de pesquisa
                endpoints.MapControllerRoute(
                    name: "categoriaFiltro",
                    pattern: "Shirt/{action}/{categoria?}",
                    defaults: new { controller = "Shirt", action = "List" }
                    );

                //página que é carregada no load da app (Homepage)
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
