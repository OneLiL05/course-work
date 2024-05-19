namespace trade_compas.Interfaces.Basic;

public interface IMatchable<TEntity>
{
    List<TEntity> Match(Func<TEntity, object> keySelector, object matcher);
}