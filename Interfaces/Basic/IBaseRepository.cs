namespace trade_compas.Interfaces.Basic;

public interface IBaseRepository<TEntity>
{
    List<TEntity> GetAll();

    TEntity? GetOne(Func<TEntity, bool> predicate);
}