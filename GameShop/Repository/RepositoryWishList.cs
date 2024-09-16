using GameShop.Repository.Interfaces;
using GameShopModel.Data;
using GameShopModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Repository;

public class RepositoryWishList(GameShopContext gameShopContext) : IRepositoryWishList
{
    public async Task AddAsync(User user, GameProduct gameProduct)
    {
        var newWishList = new WishList
        {
            GameProduct = gameProduct,
            User = user
        };

        await gameShopContext.WishLists.AddAsync(newWishList);
        await gameShopContext.SaveChangesAsync();

    }
    public async Task DeleteAsync(int idGameProduct, string idUser)
    {
        await gameShopContext.WishLists
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProduct)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProduct.Id == idGameProduct)
            .ExecuteDeleteAsync();
    }
}
