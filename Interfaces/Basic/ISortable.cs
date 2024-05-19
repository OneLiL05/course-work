using trade_compas.Enums;

namespace trade_compas.Interfaces.Basic;

public interface ISortable<TEntity>
{
    List<TEntity> SortBy(string field, SortingOrder order);
}