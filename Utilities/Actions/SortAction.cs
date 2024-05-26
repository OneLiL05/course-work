using trade_compas.Enums;

namespace trade_compas.Utilities.Actions;

public class SortAction<TEntity>
{
    public List<TEntity> DoAction(List<TEntity> list, Func<TEntity, object> keySelector, SortingOrder order)
    {
        return order == SortingOrder.Asc
            ? list.OrderBy(keySelector).ToList()
            : list.OrderByDescending(keySelector).ToList();
    }
}