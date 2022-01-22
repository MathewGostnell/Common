namespace Common.DataStructures.Graphs.Contracts;

using System.Collections.Generic;

public interface INodeSet<TKey>
{
    public IEnumerable<TKey> Nodes
    {
        get;
    }
}