using EcommerceProject.Models;

namespace EcommerceProject.Repositories.Interfaces
{
    public interface IShirtRepository
    {
        IEnumerable<Shirt> Shirts { get; }

        IEnumerable<Shirt> FavouriteShirts { get; }

        Shirt GetShirtById(int shirtId);
    }
}
