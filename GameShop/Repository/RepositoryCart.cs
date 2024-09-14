using GameShop.Repository.Interfaces;
using GameShopModel.Entities;

namespace GameShop.Repository;

public class RepositoryCart : IRepositoryCart
{
    public readonly List<GameProduct> gameProducts = [];
    public decimal Sum => 
        gameProducts.Sum(gameProducts => gameProducts.Price);
    public void Add(GameProduct gameProduct)
    {
        gameProducts.Add(gameProduct);
    }
    public void Delete(int id)
    {
        var gameProduct = gameProducts.First(product => product.Id == id);
        gameProducts.Remove(gameProduct);
    }
    public IEnumerable<GameProduct> GetProducts() => gameProducts;
    public void Clear() => gameProducts.Clear();
}
