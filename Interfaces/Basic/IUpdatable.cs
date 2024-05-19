namespace trade_compas.Interfaces.Basic;

public interface IUpdatable<TUpdateDto>
{
     void UpdateOne(int id, TUpdateDto dto);
}