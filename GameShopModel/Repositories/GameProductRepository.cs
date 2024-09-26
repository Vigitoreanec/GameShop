using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
namespace GameShopModel.Repositories;

public class GameProductRepository(GameShopContext gameShopContext) : IGameProductRepository
{
    public async Task<List<GameProduct>> GetAllAsync() =>
        await gameShopContext.GameProducts.ToListAsync();

    public async Task<GameProduct> GetByIdAsync(int id) => 
        await gameShopContext.GameProducts.FirstAsync(gameProduct => gameProduct.Id == id);

    public async Task AddAsync(GameProduct gameProduct)
    {
        await gameShopContext.GameProducts.AddAsync(gameProduct);
        await gameShopContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var deletingGameProduct = await gameShopContext.GameProducts
            .FirstAsync(gameProduct => gameProduct.Id == id);

        gameShopContext.GameProducts.Remove(deletingGameProduct);
        await gameShopContext.SaveChangesAsync();
    }

    public async Task EditAsync(int id, GameProduct gameProduct)
    {
        var editingGameProduct = await gameShopContext.GameProducts
            .FirstAsync(gameProduct => gameProduct.Id == id);

        editingGameProduct.PresentationImageUrl = gameProduct.PresentationImageUrl;
        editingGameProduct.Title = gameProduct.Title;
        editingGameProduct.Description = gameProduct.Description;
        editingGameProduct.Price = gameProduct.Price;
        editingGameProduct.ReleaseDate = gameProduct.ReleaseDate;
        editingGameProduct.Genres = gameProduct.Genres;
        editingGameProduct.Images = gameProduct.Images;

        await gameShopContext.SaveChangesAsync();
    }

    public async Task<GameProduct> GetByTitleAsync(string title) =>
       await gameShopContext.GameProducts.FirstAsync(gameProduct => gameProduct.Title == title);
    
}
