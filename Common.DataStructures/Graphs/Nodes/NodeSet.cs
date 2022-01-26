namespace Common.DataStructures.Graphs.Nodes;

using Common.DataStructures.Graphs.Contracts;
using System.Collections.Generic;

public abstract class NodeSet<TKey, TValue> : INodeSet<TKey>, INodeValueSet<TKey, TValue>
{
    protected NodeSet()
    {
    }

    public abstract IEnumerable<TKey> Nodes
    {
        get;
    }

    public abstract bool ContainsNode(
        TKey nodeKey);

    public abstract TValue? GetNodeValue(
        TKey nodeKey);

    public abstract bool SetNodeValue(
        TKey nodeKey,
        TValue? nodeValue);
}