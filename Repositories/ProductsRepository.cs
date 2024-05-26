using Supabase.Gotrue;
using trade_compas.DTOs.Product;
using trade_compas.Enums;
using trade_compas.Interfaces.Repositories;
using trade_compas.Interfaces.Helpers;
using trade_compas.Models;
using trade_compas.Utilities.Actions;
using trade_compas.Utils;

namespace trade_compas.Repositories;

public class ProductsRepository(IPathHelper pathHelper) : IProductsRepository
{
    private readonly string _collectionPath = pathHelper.GetCollectionPath("products");
    private readonly SearchAction<Product> _searchAction = new();
    private readonly DeleteAction<Product> _deleteAction = new();
    private readonly SortAction<Product> _sortAction = new();
    private readonly GetAllAction<Product> _getAllAction = new();
    private readonly GetOneAction<Product> _getOneAction = new();
    private readonly GetAllByAction<Product> _getAllByAction = new();

    public List<Product> GetAll()
    {
        return _getAllAction.DoAction(_collectionPath);
    }

    public List<Product> GetAllBy(Func<Product, bool> predicate)
    {
        return _getAllByAction.DoAction(GetAll(), predicate);
    }

    public Product? GetOne(Func<Product, bool> predicate)
    {
        return _getOneAction.DoAction(GetAll(), predicate);
    }

    public void CreateOne(CreateProductDto dto)
    {

        var products = GetAll();

        var product = new Product(dto);

        products.Add(product);

        FileHelper.SaveData(_collectionPath, products);
    }

    public void Archive(int id)
    {
        var products = GetAll();

        products
            .Where(product => product.Id == id)
            .ToList()
            .ForEach(product => product.InArchive = true);

        FileHelper.SaveData(_collectionPath, products);
    }

    public void Unarchive(int id)
    {
        var products = GetAll();

        products
            .Where(product => product.Id == id)
            .ToList()
            .ForEach(product => product.InArchive = false);


        FileHelper.SaveData(_collectionPath, products);
    }

    public void DeleteOne(int id)
    {
        _deleteAction.DoAction(_collectionPath, id);
    }

    public void UpdateOne(int id, CreateProductDto data)
    {
        var products = GetAll();

        products
            .Where(product => product.Id == id)
            .ToList()
            .ForEach(product =>
            {
                product.UpdatedAt = DateTime.Now;
                product.Name = data.Name;
                product.Description = data.Description;
                product.Price = data.Price;
                product.State = data.State;
                product.CategorySlug = data.CategorySlug;
            });

        FileHelper.SaveData(_collectionPath, products);
    }

    public List<Product> Search(Func<Product, string> keySelector, string query)
    {
        return _searchAction.DoAction(GetAll(), keySelector, query);
    }

    public List<Product> SortBy(Func<Product, object> keySelector, SortingOrder order)
    {
        return _sortAction.DoAction(GetAll(), keySelector, order);
    }

    public List<Product> SortBy(List<Product> list, Func<Product, object> keySelector, SortingOrder order)
    {
        return _sortAction.DoAction(list, keySelector, order);
    }
}

