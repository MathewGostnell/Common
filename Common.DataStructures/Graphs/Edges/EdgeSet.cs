namespace Common.DataStructures.Graphs.Edges;

using Common.DataStructures.Graphs.Contracts;
using System.Collections.Generic;

public abstract class EdgeSet<TKey, TEdge> : IEdgeSet<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public abstract IEnumerable<TEdge> Edges
    {
        get;
    }

    public abstract bool ContainsEdge(
        TKey sourceKey,
        TKey targetKey);

    public abstract bool ContainsEdge(
        TEdge edge);
}