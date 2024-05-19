using trade_compas.Enums;

namespace trade_compas.Interfaces.Basic;

public interface ISortable<TEntity>
{
    List<TEntity> SortBy(Func<TEntity, object> keySelector, SortingOrder order);

    List<TEntity> SortBy(List<TEntity> list, Func<TEntity, object> keySelector, SortingOrder order);
}