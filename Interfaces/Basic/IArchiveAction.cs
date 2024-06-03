namespace trade_compas.Interfaces.Basic;

public interface IArchiveAction
{
    void Archive(int id);

    void UnArchive(int id);
}