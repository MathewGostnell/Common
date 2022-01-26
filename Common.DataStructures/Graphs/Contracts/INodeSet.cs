namespace Common.DataStructures.Graphs.Contracts;

using System.Collections.Generic;

public interface INodeSet<TKey>
{
    public abstract bool ContainsNode(
        TKey nodeKey);

    public IEnumerable<TKey> Nodes
    {
        get;
    }
}