using trade_compas.Interfaces.Actions;
using trade_compas.Interfaces.Basic;

namespace trade_compas.Utilities.Actions;

public class DeleteAction<T> : IDeleteAction<T> where T : IIdentifiable
{
    public void DoAction(List<T> list, int id)
    {
        var item = list.Find(item => item.Id == id);

        if (item == null)
        {
            throw new KeyNotFoundException("Product with such id not found");
        }

        list.Remove(item);
    }
}