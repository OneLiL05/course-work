namespace trade_compas.Utilities.Actions;

public class GetAllByAction<TEntity>
{
    public List<TEntity> DoAction(List<TEntity> list, Predicate<TEntity> match)
    {
        return list
            .Where(entity => match(entity))
            .ToList();
    }
}