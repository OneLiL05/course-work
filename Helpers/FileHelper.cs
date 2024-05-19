using System.Text.Json;
namespace trade_compas.Utils;

public class FileHelper
{
    public static List<TEntity> LoadData<TEntity>(string path)
    {
        var json = File.ReadAllText(path);

        return JsonSerializer.Deserialize<List<TEntity>>(json) ?? [];
    }

    public static void SaveData<TEntity>(string path, List<TEntity> list)
    {
        var json = JsonSerializer.Serialize(list);

        File.WriteAllText(path, json);
    }
}