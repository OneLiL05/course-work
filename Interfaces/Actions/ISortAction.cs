using trade_compas.Enums;
using trade_compas.Models;

namespace trade_compas.Interfaces.Actions;

public interface ISortAction<TEntity>
{
    List<TEntity> DoAction(List<TEntity> list, Func<TEntity, object> keySelector, SortingOrder order);
}