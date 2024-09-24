using GameShop.Repository.Repositories.Interfaces;
using GameShopModel.Data;
using GameShopModel.Entities;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameShop.Repository.Repositories;

public class RecommendedGameProduct(GameShopContext gameShopContext) : IRecommendedGameProduct
{
    public async Task<List<RecommendedGameProducts>> GetAllAsync() =>
        await gameShopContext.RecommendedGameProducts
                    .Include(RecommendedGameProduct => RecommendedGameProduct.GameProduct)
                    .ToListAsync();

    public async Task<RecommendedGameProducts> GetByIdAsync(int id) =>
        await gameShopContext.RecommendedGameProducts
                    .Include(RecommendedGameProduct => RecommendedGameProduct.GameProduct)
                    .SingleAsync(gameProduct => gameProduct.Id == id);

    public async Task AddAsync(RecommendedGameProducts recommendedGameProduct)
    {
        await gameShopContext.RecommendedGameProducts.AddAsync(recommendedGameProduct);
        await gameShopContext.SaveChangesAsync();
    }
    public async Task RemoveAsync(int id) =>
        await gameShopContext.RecommendedGameProducts
                    .Where(gameProduct => gameProduct.Id == id)
                    .ExecuteDeleteAsync();
    //{
    //    var deletingGameProduct = await gameShopContext.RecommendedGameProducts
    //    .FirstAsync(gameProduct => gameProduct.Id == id);

    //    gameShopContext.RecommendedGameProducts.Remove(deletingGameProduct);
    //    await gameShopContext.SaveChangesAsync();
    //}

    public async Task EditAsync(int id, RecommendedGameProducts recommendedGameProduct) =>
        await gameShopContext.RecommendedGameProducts
        .Include(editrecommendedGameProduct => editrecommendedGameProduct.GameProduct)
        .Where(editrecommendedGameProduct => editrecommendedGameProduct.Id == id)
        .ExecuteUpdateAsync(s => s
        .SetProperty(
            editrecommendedGameProduct => editrecommendedGameProduct.ExpertName, recommendedGameProduct => recommendedGameProduct.ExpertName)
        .SetProperty(
            editrecommendedGameProduct => editrecommendedGameProduct.ExpertSurname, recommendedGameProduct => recommendedGameProduct.ExpertSurname)
        .SetProperty(
            editrecommendedGameProduct => editrecommendedGameProduct.GameProduct, recommendedGameProduct => recommendedGameProduct.GameProduct));
    
}
