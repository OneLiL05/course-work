using trade_compas.Enums;
using trade_compas.Interfaces.Actions;

namespace trade_compas.Utilities.Actions;

public class SortAction<TEntity> : ISortAction<TEntity>
{
    public List<TEntity> DoAction(List<TEntity> list, Func<TEntity, object> keySelector, SortingOrder order)
    {
        return order == SortingOrder.Asc
            ? list.OrderBy(keySelector).ToList()
            : list.OrderByDescending(keySelector).ToList();
    }
}