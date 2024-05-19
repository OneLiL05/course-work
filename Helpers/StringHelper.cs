namespace trade_compas.Helpers;

public class StringHelper
{
    public static string Trim(string str, int symbolsCount)
    {
        return str.Length > symbolsCount
            ? $"{str[..symbolsCount]}..."
            : str;
    }
}