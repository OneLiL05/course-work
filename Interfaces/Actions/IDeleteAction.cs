namespace trade_compas.Interfaces.Actions;

public interface IDeleteAction<TEntity>
{
    void DoAction(List<TEntity> list, int id);
}