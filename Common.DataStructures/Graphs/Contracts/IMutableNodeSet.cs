namespace Common.DataStructures.Graphs.Contracts;

public interface IMutableNodeSet<TKey> : INodeSet<TKey>
{
    public bool AddNode(
        TKey nodeKey);

    public int AddNodes(
        IEnumerable<TKey> nodeKeys);

    public void Clear();

    public bool RemoveNode(
        TKey nodeKey);

    public int RemoveNodes(
        IEnumerable<TKey> nodeKeys);
}