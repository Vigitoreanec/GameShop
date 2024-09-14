using GameShopModel.Entities;


namespace GameShop.Repository.Interfaces;

public interface IRepositoryCart
{
    decimal Sum {  get; }
    IEnumerable<GameProduct> GetProducts();
    void Add(GameProduct gameProduct);
    void Delete(int id);
    void Clear();
}
