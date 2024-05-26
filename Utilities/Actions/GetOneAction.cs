namespace trade_compas.Utilities.Actions;

public class GetOneAction<TEntity>
{
    public TEntity? DoAction(List<TEntity> list, Func<TEntity, object> keySelector, object data)
    {
        return list.Find(item => Equals(keySelector(item), data));
    }
}