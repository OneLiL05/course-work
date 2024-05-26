using trade_compas.Utils;

namespace trade_compas.Utilities.Actions;

public class UpdateAction<TEntity>
{
    public void DoAction(string collectionPath, Predicate<TEntity> match, Action<TEntity> updateAction)
    {
        var list = FileHelper.LoadData<TEntity>(collectionPath);

        list
            .Where(entity => match(entity))
            .ToList()
            .ForEach(updateAction);

        FileHelper.SaveData(collectionPath, list);
    }
}