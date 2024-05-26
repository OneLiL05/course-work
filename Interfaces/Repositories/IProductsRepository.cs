using trade_compas.DTOs.Product;
using trade_compas.Interfaces.Basic;
using trade_compas.Models;

namespace trade_compas.Interfaces.Repositories;

public interface IProductsRepository : IBaseRepository<Product>, ICreatable<CreateProductDto>, IUpdatable<CreateProductDto>, IDeletable, ISearchable<Product>, IMatchable<Product>, ISortable<Product>, IGetableBy<Product>
{
    void Archive(int id);
    void Unarchive(int id);
}