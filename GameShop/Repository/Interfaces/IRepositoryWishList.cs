using GameShopModel.Entities;

namespace GameShop.Repository.Interfaces;

public interface IRepositoryWishList
{
    Task AddAsync(User user, GameProduct gameProduct);
    Task DeleteAsync(int idGameProduct, string idUser);
}
