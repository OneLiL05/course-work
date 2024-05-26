namespace trade_compas.Utilities.Actions;

public class GetOneAction<TEntity>
{
    public TEntity? DoAction(List<TEntity> list, Func<TEntity, bool> predicate)
    {
        return list.Find(item => predicate(item));
    }
}