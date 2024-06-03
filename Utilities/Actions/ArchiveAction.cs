using trade_compas.Enums;
using trade_compas.Interfaces.Basic;

namespace trade_compas.Utilities.Actions;

public class ArchiveAction<TEntity> where TEntity : IIdentifiable, ITimestampable, IArchivable
{
    private readonly UpdateAction<TEntity> _updateAction = new();

    public void DoAction(string collectionPath, int id, ArchiveActionType actionType)
    {
        _updateAction.DoAction(collectionPath, entity => entity.Id == id, entity =>
        {
            if (actionType == ArchiveActionType.Add)
            {
                entity.IsArchived = true;
            }
            else
            {
                entity.IsArchived = false;
            }
        });
    }
}