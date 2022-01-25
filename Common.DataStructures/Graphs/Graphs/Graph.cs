namespace Common.DataStructures.Graphs.Graphs;

using Common.DataStructures.Graphs.Contracts;
using System.Collections.Generic;

public abstract class Graph<TKey, TEdge> : IGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public IEnumerable<TEdge> Edges
        => EdgeSet.Edges;

    public abstract bool IsDirected
    {
        get;
    }

    public IEnumerable<TKey> Nodes
        => NodeSet.Nodes;

    protected IEdgeSet<TKey, TEdge> EdgeSet
    {
        get;
    }

    protected INodeSet<TKey> NodeSet
    {
        get;
    }

    public Graph(
        INodeSet<TKey> nodeSet,
        IEdgeSet<TKey, TEdge> edgeSet)
    {
        if (nodeSet is null)
        {
            throw new ArgumentNullException(nameof(nodeSet));
        }

        if (edgeSet is null)
        {
            throw new ArgumentNullException(nameof(edgeSet));
        }

        EdgeSet = edgeSet;
        NodeSet = nodeSet;
    }

    public bool ContainsEdge(
        TKey sourceKey,
        TKey targetKey)
        => EdgeSet.ContainsEdge(
            sourceKey,
            targetKey);

    public bool ContainsEdge(
        TEdge edge)
        => EdgeSet.ContainsEdge(edge);

    public bool ContainsNode(
        TKey nodeKey)
        => NodeSet.ContainsNode(nodeKey);
}