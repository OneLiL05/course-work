namespace trade_compas.Utilities.Actions;

public class GetOneAction<TEntity>
{
    public TEntity? DoAction(List<TEntity> list, Predicate<TEntity> match)
    {
        return list.Find(match);
    }
}