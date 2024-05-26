using trade_compas.Interfaces.Basic;
using trade_compas.Utils;

namespace trade_compas.Utilities.Actions;

public class DeleteAction<T> where T : IIdentifiable
{
    public void DoAction(string collectionPath, int id)
    {
        var list = FileHelper.LoadData<T>(collectionPath);

        var item = list.Find(item => item.Id == id);

        if (item == null)
        {
            throw new KeyNotFoundException("Product with such id not found");
        }

        list.Remove(item);

        FileHelper.SaveData(collectionPath, list);
    }
}