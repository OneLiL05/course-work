using trade_compas.Utils;

namespace trade_compas.Utilities.Actions;

public class CreateAction<TEntity>
{
    private readonly GetAllAction<TEntity> _getAllAction = new();

    public void DoAction(string collectionPath, TEntity entity)
    {
        var list = _getAllAction.DoAction(collectionPath);

        list.Add(entity);

        FileHelper.SaveData(collectionPath, list);
    }
}