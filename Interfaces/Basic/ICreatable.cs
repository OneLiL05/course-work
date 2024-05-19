namespace trade_compas.Interfaces.Basic;

public interface ICreatable<TCreateDto>
{
    void CreateOne(TCreateDto dto);
}