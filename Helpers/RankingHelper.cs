using trade_compas.Interfaces.Basic;

namespace trade_compas.Helpers;

public class RankingHelper
{
    public static int Calculate(IReviewable entity)
    {
        return entity is { Ranking: > 0, Reviews.Count: > 0 } ? entity.Ranking / entity.Reviews.Count : 0;
    }
}