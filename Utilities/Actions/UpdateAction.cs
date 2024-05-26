using trade_compas.Utils;

namespace trade_compas.Utilities.Actions;

public class UpdateAction<TEntity>
{
    private readonly GetAllAction<TEntity> _getAllAction = new();

    public void DoAction(string collectionPath, Predicate<TEntity> match, Action<TEntity> updateAction)
    {
        var list = _getAllAction.DoAction(collectionPath);

        list
            .Where(entity => match(entity))
            .ToList()
            .ForEach(updateAction);

        FileHelper.SaveData(collectionPath, list);
    }
}