using trade_compas.Enums;
using trade_compas.Interfaces.Basic;
using trade_compas.Models;

namespace trade_compas.Utilities.Actions;

public class ReviewAction<TEntity> where TEntity : IIdentifiable, IReviewable
{
    private readonly UpdateAction<TEntity> _updateAction = new();

    public void DoAction(string collectionPath, int id, Review review, ReviewActionType actionType)
    {
        _updateAction.DoAction(
            collectionPath,
            entity => entity.Id == id,
            entity =>
            {
                if (actionType == ReviewActionType.Add)
                {
                    entity.Reviews.Add(review);
                    entity.Ranking += review.Stars;
                }
                else
                {
                    entity.Reviews = entity.Reviews.Where(c => c.Id != review.Id).ToList();
                    entity.Ranking -= review.Stars;
                }
            });
    }
}