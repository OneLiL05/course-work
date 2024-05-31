using trade_compas.DTOs.Product;
using trade_compas.Enums;
using trade_compas.Interfaces.Repositories;
using trade_compas.Interfaces.Helpers;
using trade_compas.Models;
using trade_compas.Utilities.Actions;

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
    private readonly UpdateAction<Product> _updateAction = new();
    private readonly CreateAction<Product> _createAction = new();

    public List<Product> GetAll()
    {
        return _getAllAction.DoAction(_collectionPath);
    }

    public List<Product> GetAllBy(Predicate<Product> match)
    {
        return _getAllByAction.DoAction(GetAll(), match);
    }

    public Product? GetOne(Predicate<Product> match)
    {
        return _getOneAction.DoAction(GetAll(), match);
    }

    public void CreateOne(CreateProductDto dto)
    {
        var product = new Product(dto);

        _createAction.DoAction(_collectionPath, product);
    }

    public void Archive(int id)
    {
        _updateAction.DoAction(
            _collectionPath,
            product => product.Id == id,
            product => product.InArchive = true);
    }

    public void Unarchive(int id)
    {
        _updateAction.DoAction(
            _collectionPath,
            product => product.Id == id,
            product => product.InArchive = false);
    }

    public void DeleteOne(int id)
    {
        _deleteAction.DoAction(_collectionPath, id);
    }

    public void UpdateOne(int id, CreateProductDto data)
    {
        _updateAction.DoAction(
            _collectionPath,
            product => product.Id == id,
            product =>
            {
                product.UpdatedAt = DateTime.Now;
                product.Name = data.Name;
                product.Description = data.Description;
                product.Price = data.Price;
                product.State = data.State;
                product.CategorySlug = data.CategorySlug;
            });
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

    public void AddComment(int id, Comment comment)
    {
        _updateAction.DoAction(
            _collectionPath,
            product => product.Id == id,
            product =>
            {
                product.Comments.Add(comment);
            });
    }

    public void RemoveComment(int id, Comment comment)
    {
        Console.WriteLine(comment.Id);

        _updateAction.DoAction(
            _collectionPath,
            product => product.Id == id,
            product =>
            {
                product.Comments = product.Comments.Where(c => c.Id != comment.Id).ToList();
            });
    }
}
