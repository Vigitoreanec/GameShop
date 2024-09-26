
using GameShopModel.Entities;

namespace GameShopModel.Repositories.Interface;

public interface IGameProductRepository
{
    Task<List<GameProduct>> GetAllAsync();
    Task<GameProduct> GetByIdAsync(int id);
    Task<GameProduct> GetByTitleAsync(string title);
    Task AddAsync(GameProduct gameProduct);
    Task RemoveAsync(int id);
    Task EditAsync(int id, GameProduct gameProduct);
}
