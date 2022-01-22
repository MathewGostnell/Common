namespace Common.DataStructures.Graphs.Contracts;

public interface IImplicitNodeSet<TKey>
{
    public bool ContainsNode(
        TKey nodeKey);
}