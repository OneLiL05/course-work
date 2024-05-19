namespace trade_compas.Interfaces.Actions;

public interface ISearchAction<TEntity>
{
    List<TEntity> DoAction(List<TEntity> list, Func<TEntity, string> keySelector, string query);
}