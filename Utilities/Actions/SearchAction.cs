namespace trade_compas.Utilities.Actions;

public class SearchAction<T>
{
    public List<T> DoAction(List<T> list, Func<T, string> keySelector, string query)
    {
        return list.Where(item =>
        {
            var value = keySelector(item);

            return value.Contains(query, StringComparison.CurrentCultureIgnoreCase);
        }).ToList();
    }
}