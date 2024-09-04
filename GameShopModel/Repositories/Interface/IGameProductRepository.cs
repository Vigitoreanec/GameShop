
using GameShopModel.Entities;

namespace GameShopModel.Repositories.Interface;

public interface IGameProductRepository
{
    Task<List<GameProduct>> GetAllGameProductsAsync();
    Task<GameProduct> GetGameProductAsync(int id);
    Task AddGameProductAsync(GameProduct gameProduct);
    Task RemoveGameProductAsync(int id);
    Task EditGameProductAsync(int id, GameProduct gameProduct);
}
