using trade_compas.Models;

namespace trade_compas.Interfaces.Basic;

public interface IReviewable
{
    List<Review> Reviews { get; set; }
    int Ranking { get; set; }
}