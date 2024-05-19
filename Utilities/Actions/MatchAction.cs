using trade_compas.Interfaces.Actions;

namespace trade_compas.Utilities.Actions;

public class MatchAction<T> : IMatchAction<T>
{
    public List<T> DoAction(List<T> list, Func<T, object> keySelector, object matcher)
    {
        return list.Where(item =>
        {
            var value = keySelector(item);

            return Equals(value, matcher);
        }).ToList();
    }
}