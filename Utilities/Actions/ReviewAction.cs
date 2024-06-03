using trade_compas.Enums;
using trade_compas.Interfaces.Basic;
using trade_compas.Models;

namespace trade_compas.Utilities.Actions;

public class ReviewAction<TEntity> where TEntity : IIdentifiable, IReviewable, ITimestampable
{
    private readonly UpdateAction<TEntity> _updateAction = new();

    public void DoAction(string collectionPath, int id, Review review, ReviewActionType actionType)
    {
        _updateAction.DoAction(
            collectionPath,
            entity => entity.Id == id,
            entity =>
            {
                switch (actionType)
                {
                    case ReviewActionType.Add:
                        entity.Reviews.Add(review);
                        entity.Ranking += review.Stars;
                        break;
                    case ReviewActionType.Remove:
                        entity.Reviews = entity.Reviews.Where(c => c.Id != review.Id).ToList();
                        entity.Ranking -= review.Stars;
                        break;
                    case ReviewActionType.Update:
                        entity.Reviews.Where(r => r.Id == review.Id).ToList().ForEach(r =>
                        {
                            r.Content = review.Content;
                            r.Stars = review.Stars;
                            r.IsEdited = review.IsEdited;
                        });
                        break;
                }
            });
    }
}