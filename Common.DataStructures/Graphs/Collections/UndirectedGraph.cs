namespace Common.DataStructures.Graphs.Collections;

using Common.DataStructures.Graphs.Actions;
using Common.DataStructures.Graphs.Contracts;
using Common.DataStructures.Graphs.Predicates;
using Common.ExtensionLibrary;
using System.Collections.Generic;

public class UndirectedGraph<TKey, TEdge>
    : IMutableUndirectedGraph<TKey, TEdge>,
        ICloneable
    where TEdge : IEdge<TKey>
    where TKey : IEquatable<TKey>
{
    public bool AreEdgesEmpty
        => Edges.IsEmpty();

    public bool AreNodesEmpty
        => Nodes.IsEmpty();

    public event EdgeAction<TKey, TEdge>? EdgeAdded;

    public int EdgeCapacity
    {
        get;
        set;
    }

    public int EdgeCount
    {
        get;
        private set;
    }

    public EdgeEqualityComparer<TKey, TEdge> EdgeEqualityComparer
    {
        get;
        private set;
    }

    public event EdgeAction<TKey, TEdge>? EdgeRemoved;

    public IEnumerable<TEdge> Edges
        => GetEdges();

    private IEnumerable<TEdge> GetEdges()
    {
        foreach (
            IEdgeList<TKey, TEdge>? edgeLists
            in NodeToEdgesMapping.Values)
        {
            foreach (
                TEdge? edge in edgeLists)
            {
                yield return edge;
            }
        }
    }

    public bool IsDirected => false;

    public event NodeAction<TKey>? NodeAdded;

    public int NodeCount
        => Nodes.Count();

    public event NodeAction<TKey>? NodeRemoved;

    public IEnumerable<TKey> Nodes
        => NodeToEdgesMapping.Keys;

    public readonly NodeEdgeDictionary<TKey, TEdge> NodeToEdgesMapping = new();

    public UndirectedGraph(
        EdgeEqualityComparer<TKey, TEdge> edgeEqualityComparer)
    {
        if (edgeEqualityComparer is null)
        {
            throw new ArgumentNullException(nameof(edgeEqualityComparer));
        }

        EdgeEqualityComparer = edgeEqualityComparer;
    }

    private UndirectedGraph(
        NodeEdgeDictionary<TKey, TEdge> nodeToEdgesMapping,
        EdgeEqualityComparer<TKey, TEdge> edgeEqualityComparer,
        int edgeCount,
        int edgeCapacity)
    {
        if (nodeToEdgesMapping is null)
        {
            throw new ArgumentNullException(nameof(nodeToEdgesMapping));
        }

        if (edgeEqualityComparer is null)
        {
            throw new ArgumentNullException(nameof(edgeEqualityComparer));
        }

        if (edgeCount.IsLessThanZero())
        {
            throw new ArgumentOutOfRangeException(nameof(edgeCount));
        }

        EdgeCapacity = edgeCapacity;
        EdgeCount = edgeCount;
        EdgeEqualityComparer = edgeEqualityComparer;
        NodeToEdgesMapping = nodeToEdgesMapping;
    }

    public bool AddEdge(
        TEdge edge)
    {
        IEdgeList<TKey, TEdge>? existingEdges = NodeToEdgesMapping[edge.Source];
        existingEdges.Add(edge);
        if (edge.IsNotSelfEdge())
        {
            IEdgeList<TKey, TEdge>? targetEdges = NodeToEdgesMapping[edge.Target];
            targetEdges.Add(edge);
        }

        EdgeCount++;
        OnEdgeAdded(edge);

        return true;
    }

    public int AddEdges(
        IEnumerable<TEdge> edges)
        => edges.Count(AddEdge);

    public bool AddNode(
        TKey nodeKey)
    {
        if (nodeKey is null)
        {
            throw new ArgumentNullException(nameof(nodeKey));
        }

        if (ContainsNode(nodeKey))
        {
            return false;
        }

        EdgeList<TKey, TEdge>? nodeEdges = EdgeCapacity.IsLessThanZero()
            ? new EdgeList<TKey, TEdge>()
            : new EdgeList<TKey, TEdge>(EdgeCapacity);
        NodeToEdgesMapping.Add(
            nodeKey,
            nodeEdges);
        OnNodeAdded(nodeKey);

        return true;
    }

    public int AddNodes(
        IEnumerable<TKey> nodeKeys)
        => nodeKeys.Count(AddNode);

    public bool AddNodesAndEdge(
        TEdge edge)
    {
        IEdgeList<TKey, TEdge>? sourceEdges = AddAndReturnEdges(edge.Source);
        IEdgeList<TKey, TEdge>? targetEdges = AddAndReturnEdges(edge.Target);
        if (sourceEdges is null
            || targetEdges is null)
        {
            return false;
        }

        sourceEdges.Add(edge);
        if (edge.IsNotSelfEdge())
        {
            targetEdges.Add(edge);
        }

        EdgeCount++;
        OnEdgeAdded(edge);

        return true;
    }

    public int AddNodesAndEdges(
        IEnumerable<TEdge> edges)
        => edges.Count(AddNodesAndEdge);

    public int AdjacentDegree(
        TKey nodeKey)
        => ContainsNode(nodeKey)
            ? NodeToEdgesMapping[nodeKey].Count
            : -1;

    public TEdge AdjacentEdge(
        TKey sourceKey,
        int adjacentEdgeIndex)
        => NodeToEdgesMapping[sourceKey][adjacentEdgeIndex];

    public IEnumerable<TEdge> AdjacentEdges(
        TKey nodeKey)
        => NodeToEdgesMapping[nodeKey];

    public IEnumerable<TKey> AdjacentVertices(
        TKey nodeKey)
    {
        IEnumerable<TEdge>? adjacentEdges = AdjacentEdges(nodeKey);
        var adjacentVertices = new HashSet<TKey>();
        foreach (
            TEdge edge in adjacentEdges)
        {
            adjacentVertices.Add(edge.Source);
            adjacentVertices.Add(edge.Target);
        }

        adjacentVertices.Remove(nodeKey);

        return adjacentVertices;
    }

    public void Clear()
    {
        EdgeCount = 0;
        NodeToEdgesMapping.Clear();
    }

    public void ClearAdjacentEdges(
        TKey nodeKey)
    {
        if (!ContainsNode(nodeKey))
        {
            return;
        }

        IEdgeList<TKey, TEdge>? adjacentEdges = NodeToEdgesMapping[nodeKey];
        EdgeCount -= adjacentEdges.Count;

        foreach (
            TEdge? adjacentEdge in adjacentEdges)
        {
            if (NodeToEdgesMapping.TryGetValue(
                adjacentEdge.Target,
                out IEdgeList<TKey, TEdge>? nestedEdges))
            {
                nestedEdges.Remove(adjacentEdge);
            }

            if (NodeToEdgesMapping.TryGetValue(
                adjacentEdge.Source,
                out nestedEdges))
            {
                nestedEdges.Remove(adjacentEdge);
            }
        }
    }

    public UndirectedGraph<TKey, TEdge> Clone()
        => new(
            NodeToEdgesMapping.Clone(),
            EdgeEqualityComparer,
            EdgeCount,
            EdgeCapacity);

    object ICloneable.Clone()
        => Clone();

    public bool ContainsEdge(
                TKey sourceKey,
        TKey targetKey)
        => TryGetEdge(
            sourceKey,
            targetKey,
            out TEdge? edge);

    public bool ContainsNode(TKey nodeKey)
        => NodeToEdgesMapping.ContainsKey(nodeKey);

    public bool IsAdjacentEdgesEmpty(
        TKey nodeKey)
        => NodeToEdgesMapping[nodeKey].IsEmpty();

    public virtual void OnEdgeRemoved(
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

    public virtual void OnNodeRemoved(
        TKey nodeKey)
    {
        if (nodeKey is null)
        {
            throw new ArgumentNullException(nameof(nodeKey));
        }

        if (NodeRemoved is null)
        {
            return;
        }

        NodeAction<TKey>? nodeRemovedEventHandler = NodeRemoved;
        nodeRemovedEventHandler(nodeKey);
    }

    public int RemoveAdjacentEdge(
                TKey nodeKey,
        EdgePredicate<TKey, TEdge> predicate)
    {
        IEdgeList<TKey, TEdge>? adjacentEdges = NodeToEdgesMapping[nodeKey];
        var edgeList = new List<TEdge>(adjacentEdges.Count);

        foreach (
            TEdge? adjacentEdge in adjacentEdges)
        {
            if (adjacentEdge is null)
            {
                continue;
            }

            if (predicate(adjacentEdge))
            {
                edgeList.Add(adjacentEdge);
            }
        }

        RemoveEdges(edgeList);

        return edgeList.Count;
    }

    public bool RemoveEdge(
        TEdge edge)
    {
        if (!ContainsEdge(
            edge.Source,
            edge.Target))
        {
            return false;
        }

        bool isEdgeRemoved = NodeToEdgesMapping[edge.Source].Remove(edge);
        if (!isEdgeRemoved)
        {
            return false;
        }

        if (edge.IsNotSelfEdge())
        {
            NodeToEdgesMapping[edge.Target].Remove(edge);
        }

        EdgeCount--;
        if (EdgeCount.IsLessThanZero())
        {
            throw new ApplicationException(
                "Invalid graph set encountered: cannot have negative edge count.");
        }

        OnEdgeRemoved(edge);

        return true;
    }

    public int RemoveEdge(
        EdgePredicate<TKey, TEdge> predicate)
    {
        var edgeList = new List<TEdge>();
        foreach (
            TEdge? edge in Edges)
        {
            if (edge is null)
            {
                continue;
            }

            if (predicate(edge))
            {
                edgeList.Add(edge);
            }
        }

        return RemoveEdges(edgeList);
    }

    public int RemoveEdges(
        IEnumerable<TEdge> edges)
        => edges.Count(RemoveEdge);

    public bool RemoveNode(
        TKey nodeKey)
    {
        if (!ContainsNode(nodeKey))
        {
            return false;
        }

        ClearAdjacentEdges(nodeKey);
        if (NodeToEdgesMapping.Remove(nodeKey))
        {
            OnNodeRemoved(nodeKey);
            return true;
        }

        return false;
    }

    public int RemoveNodes(
        IEnumerable<TKey> nodeKeys)
        => nodeKeys.Count(RemoveNode);

    public int RemoveNodes(
        NodePredicate<TKey> predicate)
    {
        var nodeList = new List<TKey>();
        foreach (
            TKey? node in Nodes)
        {
            if (node is null)
            {
                continue;
            }

            if (predicate(node))
            {
                nodeList.Add(node);
            }
        }

        return RemoveNodes(nodeList);
    }

    public bool TryGetEdge(
        TKey sourceKey,
        TKey targetKey,
        out TEdge? edge)
    {
        bool isKeySwapNeeded = Comparer<TKey>.Default
            .Compare(
                sourceKey,
                targetKey)
            .IsGreaterThanZero();
        if (isKeySwapNeeded)
        {
            (targetKey, sourceKey) = (sourceKey, targetKey);
        }

        foreach (
            TEdge? foundEdge in NodeToEdgesMapping[sourceKey])
        {
            if (EdgeEqualityComparer(
                foundEdge,
                sourceKey,
                targetKey))
            {
                edge = foundEdge;

                return true;
            }
        }

        edge = default;

        return false;
    }

    protected virtual void OnEdgeAdded(
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

    protected virtual void OnNodeAdded(
        TKey nodeKey)
    {
        if (nodeKey is null)
        {
            throw new ArgumentNullException(nameof(nodeKey));
        }

        if (NodeAdded is null)
        {
            return;
        }

        NodeAction<TKey>? nodeAddedEventHandler = NodeAdded;
        nodeAddedEventHandler(nodeKey);
    }

    private IEdgeList<TKey, TEdge>? AddAndReturnEdges(
        TKey nodeKey)
    {
        if (!NodeToEdgesMapping.TryGetValue(
            nodeKey,
            out IEdgeList<TKey, TEdge>? edgeList))
        {
            NodeToEdgesMapping.Add(
                nodeKey,
                EdgeCapacity.IsLessThanZero()
                ? new EdgeList<TKey, TEdge>()
                : new EdgeList<TKey, TEdge>(EdgeCapacity));
        }

        return edgeList;
    }
}