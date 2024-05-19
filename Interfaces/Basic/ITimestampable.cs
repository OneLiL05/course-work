namespace trade_compas.Interfaces.Basic;

public interface ITimestampable
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
