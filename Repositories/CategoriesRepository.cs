using trade_compas.Interfaces;
using trade_compas.Interfaces.Helpers;
using trade_compas.Models;
using trade_compas.Utils;

namespace trade_compas.Repositories;

public class CategoriesRepository(IPathHelper pathHelper) : ICategoriesRepository
{
    private readonly string _collectionPath = pathHelper.GetCollectionPath("categories");
    public List<Category> GetAll()
    {
        return FileHelper.LoadData<Category>(_collectionPath);
    }

    public Category? GetOne(int id)
    {
        return GetAll().Find(category => category.Id == id);
    }

    public Category? GetOne(string slug)
    {
        return GetAll().Find(category => category.Slug == slug);
    }
}