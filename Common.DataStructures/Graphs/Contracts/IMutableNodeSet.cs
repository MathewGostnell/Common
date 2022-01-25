namespace Common.DataStructures.Graphs.Contracts;

public interface IMutableNodeSet<TKey, TValue> : INodeValueSet<TKey, TValue>
{
    public bool AddNode(
        TKey nodeKey);

    public int AddNodes(
        IEnumerable<TKey> nodeKeys);

    public bool Clear();

    public bool RemoveNode(
        TKey nodeKey);

    public int RemoveNodes(
        IEnumerable<TKey> nodeKeys);
}