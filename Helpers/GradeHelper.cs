using trade_compas.Interfaces.Basic;

namespace trade_compas.Helpers;

public class GradeHelper<TEntity> where TEntity : IGradable
{
    public static int GetGradeCount(IEnumerable<TEntity> list, int grade)
    {
        return list.Count(entity => entity.Grade == grade);
    }
}