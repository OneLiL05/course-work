using trade_compas.Models;

namespace trade_compas.Interfaces.Basic;

public interface IReviewAction
{
    void AddReview(int id, Review review);

    void RemoveReview(int id, Review review);
}