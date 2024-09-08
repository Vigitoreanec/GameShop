using GameShopModel.Entities;


namespace GameShop.Repository.Interfaces;

public interface IRepositoryCart
{
    IEnumerable<GameProduct> GetProducts();
    void Add(GameProduct gameProduct);
    void Delete(int id);
}
