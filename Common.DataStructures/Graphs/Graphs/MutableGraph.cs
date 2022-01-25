namespace Common.DataStructures.Graphs.Graphs;

using Common.DataStructures.Graphs.Contracts;
using Common.DataStructures.Graphs.Edges;
using Common.DataStructures.Graphs.Nodes;
using Common.ExtensionLibrary;

public class MutableGraph<TKey, TEdge>
    : Graph<TKey, TEdge>,
        IMutableGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public MutableGraph(
        IMutableNodeSet<TKey> nodeSet,
        IMutableEdgeSet<TKey, TEdge> edgeSet,
        EdgeFactory<TKey, TEdge> edgeFactory,
        bool isDirected = false)
        : base(
            nodeSet,
            edgeSet)
    {
        if (nodeSet is null)
        {
            throw new ArgumentNullException(nameof(nodeSet));
        }

        if (edgeSet is null)
        {
            throw new ArgumentNullException(nameof(edgeSet));
        }

        if (edgeFactory is null)
        {
            throw new ArgumentNullException(nameof(edgeFactory));
        }

        EdgeFactory = edgeFactory;
        EdgeSet = edgeSet;
        IsDirected = isDirected;
        NodeSet = nodeSet;
    }

    public override bool IsDirected
    {
        get;
    }

    protected new IMutableEdgeSet<TKey, TEdge> EdgeSet
    {
        get;
    }

    protected new IMutableNodeSet<TKey> NodeSet
    {
        get;
    }

    protected IEdgeFactory<TKey, TEdge> EdgeFactory
    {
        get;
    }

    public event EdgeAction<TKey, TEdge>? EdgeAdded;

    public event EdgeAction<TKey, TEdge>? EdgeRemoved;

    public event NodeAction<TKey>? NodeAdded;

    public event NodeAction<TKey>? NodeRemoved;

    public bool AddEdge(
        TKey sourceKey,
        TKey targetKey)
    {
        if (!ContainsNode(sourceKey)
            || !ContainsNode(targetKey)
            || ContainsEdge(
                sourceKey,
                targetKey))
        {
            return false;
        }

        bool addedEdge = IsDirected
        ? EdgeSet.AddEdge(
            sourceKey,
            targetKey)
        : EdgeSet.AddEdge(
                sourceKey,
                targetKey)
            && EdgeSet.AddEdge(
                targetKey,
                sourceKey);
        if (addedEdge)
        {
            OnEdgeAdded(
                EdgeFactory.CreateEdge(
                    sourceKey,
                    targetKey));
        }

        return addedEdge;
    }

    public int AddEdges(
        IEnumerable<TEdge>? edges)
    {
        int addedEdges = EdgeSet.AddEdges(edges);
        if (IsDirected
            || addedEdges.IsLessThanOrEqualTo(0))
        {
            return addedEdges;
        }

        IEnumerable<TEdge>? otherEdges = edges?.Select(
            edge =>
            EdgeFactory.CreateEdge(
                edge.TargetKey,
                edge.SourceKey));
        if (otherEdges is null)
        {
            return addedEdges;
        }

        addedEdges += EdgeSet.AddEdges(otherEdges);

        return addedEdges;
    }

    public bool AddNode(
        TKey nodeKey)
        => NodeSet.AddNode(nodeKey);

    public int AddNodes(
        IEnumerable<TKey> nodeKeys)
        => NodeSet.AddNodes(nodeKeys);

    public bool AreAdjacent(
        TKey sourceKey,
        TKey targetKey)
    {
        bool areAdjacent = EdgeSet.AreAdjacent(
            sourceKey,
            targetKey);

        return IsDirected
            ? areAdjacent
            : EdgeSet.AreAdjacent(
                targetKey,
                sourceKey)
            && areAdjacent;
    }

    public void Clear()
    {
        EdgeSet.Clear();
        NodeSet.Clear();
    }

    public IEnumerable<TEdge> GetNeighbors(
        TKey sourceKey)
    {
        IEnumerable<TEdge>? neighbors = EdgeSet.GetNeighbors(sourceKey);
        if (IsDirected)
        {
            return neighbors;
        }

        foreach (
            TEdge? neighbor in neighbors)
        {
            neighbors = neighbors.Union(
                EdgeSet
                    .GetNeighbors(neighbor.TargetKey)
                    .Where(
                        reversedNeighbor =>
                        reversedNeighbor.TargetKey is not null
                        && !reversedNeighbor.TargetKey.Equals(sourceKey)));
        }

        // TODO if self edge add it back;
        neighbors = neighbors.Distinct();
        return neighbors;
    }

    public virtual void OnEdgeAdded(
        TEdge edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        if (EdgeAdded is null)
        {
            return;
        }

        EdgeAction<TKey, TEdge>? edgeAddedEventHandler = EdgeAdded;
        edgeAddedEventHandler(edge);
    }

    public void OnEdgeRemoved(
        TEdge edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        if (EdgeRemoved is null)
        {
            return;
        }

        EdgeAction<TKey, TEdge>? edgeRemovedEventHandler = EdgeRemoved;
        edgeRemovedEventHandler(edge);
    }

    public void OnNodeAdded(
        TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (NodeAdded is null)
        {
            return;
        }

        NodeAction<TKey>? nodeAddedEventHandler = NodeAdded;
        nodeAddedEventHandler(key);
    }

    public void OnNodeRemoved(
        TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (NodeRemoved is null)
        {
            return;
        }

        NodeAction<TKey>? nodeRemovedEventHandler = NodeRemoved;
        nodeRemovedEventHandler(key);
    }

    public bool RemoveEdge(
        TKey sourceKey,
        TKey targetKey)
    {
        bool removedEdge = EdgeSet.RemoveEdge(
            sourceKey,
            targetKey);

        return IsDirected
            ? removedEdge
            : EdgeSet.RemoveEdge(
                targetKey,
                sourceKey)
            && removedEdge;
    }

    public int RemoveEdges(
        IEnumerable<TEdge> edges)
    {
        int removedEdges = EdgeSet.RemoveEdges(edges);
        if (IsDirected)
        {
            return removedEdges;
        }

        IEnumerable<TEdge>? otherEdges = edges.Select(
            edge =>
            EdgeFactory.CreateEdge(
                edge.TargetKey,
                edge.SourceKey));
        if (otherEdges is null)
        {
            return removedEdges;
        }

        removedEdges += EdgeSet.RemoveEdges(otherEdges);

        return removedEdges;
    }

    public bool RemoveNode(
        TKey nodeKey)
        => NodeSet.RemoveNode(nodeKey);

    public int RemoveNodes(
        IEnumerable<TKey> nodeKeys)
        => NodeSet.RemoveNodes(nodeKeys);
}