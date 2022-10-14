using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Repositories
{
    public class ShirtRepository : IShirtRepository
    {

        private readonly AppDbContext _context;
        public ShirtRepository(AppDbContext context)
        {
            _context = context;
        }

        //Pegar todos as camisolas e categorias
        public IEnumerable<Shirt> Shirts => _context.Shirts.Include(c => c.Categoria);

        //Pegar todos as camisolas favoritas e respetivas categorias
        public IEnumerable<Shirt> FavouriteShirts => _context.Shirts.Where(l => l.IsFavourite).Include(c => c.Categoria);

        //Pegar uma camisola pelo Id
        public Shirt GetShirtById(int shirtId)
        {
            return _context.Shirts.FirstOrDefault(s => s.ShirtId==shirtId);
        }
    }
}
