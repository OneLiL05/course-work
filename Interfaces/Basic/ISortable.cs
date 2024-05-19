using trade_compas.Enums;

namespace trade_compas.Interfaces.Basic;

public interface ISortable<TEntity>
{
    List<TEntity> SortBy(Func<TEntity, object> keySelector, SortingOrder order);
}