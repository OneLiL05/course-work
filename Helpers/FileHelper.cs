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
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        var json = JsonSerializer.Serialize(list, serializeOptions);

        File.WriteAllText(path, json);
    }
}