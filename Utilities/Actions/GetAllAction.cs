using trade_compas.Utils;

namespace trade_compas.Utilities.Actions;

public class GetAllAction<TEntity>
{
    public List<TEntity> DoAction(string collectionPath)
    {
        return FileHelper.LoadData<TEntity>(collectionPath);
    }
}