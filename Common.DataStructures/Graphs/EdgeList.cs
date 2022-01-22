namespace Common.DataStructures.Graphs;

using Common.DataStructures.Graphs.Contracts;
using System;

public class EdgeList<TKey, TEdge>
    : List<TEdge>,
        ICloneable,
        IEdgeList<TKey, TEdge>
    where TKey : IEquatable<TKey>
    where TEdge : IEdge<TKey>
{
    public EdgeList()
    {
    }

    public EdgeList(
        int capacity)
        : base(capacity)
    {
    }

    public EdgeList(
        EdgeList<TKey, TEdge> edgeList)
        : base(edgeList)
    {
    }

    public EdgeList<TKey, TEdge> Clone()
        => new(this);

    IEdgeList<TKey, TEdge> IEdgeList<TKey, TEdge>.Clone() => Clone();

    object ICloneable.Clone() => Clone();
}