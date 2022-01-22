namespace Common.DataStructures.Graphs;
public sealed class NodeList<TKey>
    : List<TKey>,
        ICloneable
{
    public NodeList()
    {
    }

    public NodeList(
        int capacity)
        : base(capacity)
    {
    }

    public NodeList(
        NodeList<TKey> nodeList)
        : base(nodeList)
    {
    }

    public NodeList<TKey> Clone()
        => new(this);

    object ICloneable.Clone()
        => Clone();
}