namespace trade_compas.Utilities.Actions;

public class GetAllByAction<TEntity>
{
    public List<TEntity> DoAction(List<TEntity> list, Func<TEntity, bool> predicate)
    {
        return list
            .Where(predicate)
            .ToList();
    }
}