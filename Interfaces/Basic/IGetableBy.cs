namespace trade_compas.Interfaces.Basic;

public interface IGetableBy<TEntity>
{
    List<TEntity> GetAllBy(Func<TEntity, bool> predicate);
}