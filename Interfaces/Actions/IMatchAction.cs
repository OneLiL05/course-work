namespace trade_compas.Interfaces.Actions;

public interface IMatchAction<TEntity>
{
    List<TEntity> DoAction(List<TEntity> list, Func<TEntity, object> keySelector, object matcher);
}