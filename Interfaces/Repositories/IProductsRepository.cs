using trade_compas.DTOs.Product;
using trade_compas.Interfaces.Basic;
using trade_compas.Models;

namespace trade_compas.Interfaces.Repositories;

public interface IProductsRepository : IBaseRepository<Product>, ICreatable<CreateProductDto>, IUpdatable<CreateProductDto>, IDeletable, ISearchable<Product>, IMatchable<Product>, ISortable<Product>
{
    void Archive(int id);
    List<Product> GetArchive(string userId);
    void Unarchive(int id);
    List<Product> GetUserProducts(string userId);

    List<Product> GetUnarchived();
}