namespace Common.DataStructures.Graphs.Contracts;

using System.Collections.Generic;

public interface IEdgeSet<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public bool AreEdgesEmpty
    {
        get;
    }

    public int EdgeCount
    {
        get;
    }

    public IEnumerable<TEdge> Edges
    {
        get;
    }
}