using GameShopModel.Entities;


namespace GameShop.Repository.Repositories.Interfaces;

public interface IRecommendedGameProduct
{
    Task<List<RecommendedGameProducts>> GetAllAsync();
    Task<RecommendedGameProducts> GetByIdAsync(int id);
    Task AddAsync(RecommendedGameProducts gameProduct);
    Task RemoveAsync(int id);
    Task EditAsync(int id, RecommendedGameProducts gameProduct);
}
