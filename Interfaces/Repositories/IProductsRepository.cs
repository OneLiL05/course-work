using trade_compas.DTOs.Product;
using trade_compas.Interfaces.Basic;
using trade_compas.Models;

namespace trade_compas.Interfaces;

public interface IProductsRepository : IBaseRepository<Product>, ICreatable<CreateProductDto>, IUpdatable<CreateProductDto>, IDeletable, ISearchable<Product>, IMatchable<Product>
{
    List<Product> GetAllByCategory(string slug);

    List<Product> GetUserArchive(string userId);
}