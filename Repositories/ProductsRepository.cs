using Supabase.Gotrue;
using trade_compas.DTOs.Product;
using trade_compas.Enums;
using trade_compas.Interfaces.Repositories;
using trade_compas.Interfaces.Helpers;
using trade_compas.Models;
using trade_compas.Utilities.Actions;
using trade_compas.Utils;

namespace trade_compas.Repositories;

public class ProductsRepository(IPathHelper pathHelper, Supabase.Client supabaseClient) : IProductsRepository
{
    private readonly string _collectionPath = pathHelper.GetCollectionPath("products");
    private readonly User? _user = supabaseClient.Auth.CurrentUser;
    private readonly SearchAction<Product> _searchAction = new();
    private readonly MatchAction<Product> _matchAction = new();
    private readonly DeleteAction<Product> _deleteAction = new();
    private readonly SortAction<Product> _sortAction = new();

    public List<Product> GetAll()
    {
        return FileHelper.LoadData<Product>(_collectionPath);
    }

    public List<Product> GetAllByCategory(string slug)
    {
        return GetAll()
            .Where(product => product.CategorySlug == slug)
            .ToList();
    }

    public Product? GetOne(int id)
    {
        return GetAll().Find(product => product.Id == id);
    }

    public void CreateOne(CreateProductDto data)
    {

        var products = GetAll();

        var product = new Product()
        {
            Id = products.Count + 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Name = data.Name,
            Description = string.IsNullOrEmpty(data.Description) ? "" : data.Description,
            Price = data.Price,
            State = data.State,
            CategorySlug = data.CategorySlug,
            SellerId = _user?.Id!,
        };

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

    public List<Product> GetArchive(string userId)
    {
        return GetAll()
            .Where(product => product.SellerId == userId && product.InArchive)
            .ToList();
    }

    public List<Product> GetUnarchived()
    {
        return GetAll()
            .Where(product => !product.InArchive)
            .ToList();
    }

    public List<Product> GetUserProducts(string userId)
    {
        return GetAll()
            .Where(product => product.SellerId == userId && !product.InArchive)
            .ToList();
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
        var products = GetAll();

        _deleteAction.DoAction(products, id);

        FileHelper.SaveData(_collectionPath, products);
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

    public List<Product> Match(Func<Product, object> keySelector, object matcher)
    {
        return _matchAction.DoAction(GetAll(), keySelector, matcher);
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

