using trade_compas.Interfaces;
using trade_compas.Interfaces.Helpers;
using trade_compas.Models;
using trade_compas.Utilities.Actions;
using trade_compas.Utils;

namespace trade_compas.Repositories;

public class CategoriesRepository(IPathHelper pathHelper) : ICategoriesRepository
{
    private readonly string _collectionPath = pathHelper.GetCollectionPath("categories");
    private readonly GetOneAction<Category> _getOneAction = new();

    public List<Category> GetAll()
    {
        return FileHelper.LoadData<Category>(_collectionPath);
    }

    public Category? GetOne(Func<Category, bool> predicate)
    {
        return _getOneAction.DoAction(GetAll(), predicate);
    }
}