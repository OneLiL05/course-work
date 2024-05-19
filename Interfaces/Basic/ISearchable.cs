using trade_compas.Models;

namespace trade_compas.Interfaces.Basic;

public interface ISearchable<TEntity>
{
    List<TEntity> Search(Func<Product, string> keySelector, string query);
}
