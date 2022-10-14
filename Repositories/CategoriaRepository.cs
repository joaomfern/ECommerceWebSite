using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interfaces;

namespace EcommerceProject.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        //Pegar todos as categorias
        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
