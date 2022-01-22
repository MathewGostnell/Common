namespace Common.DataStructures.Graphs.Nodes;

using Common.DataStructures.Graphs.Contracts;
using System.Collections.Generic;

public abstract class NodeSet<TKey> : INodeSet<TKey>
{
    public abstract IEnumerable<TKey> Nodes
    {
        get;
    }
}