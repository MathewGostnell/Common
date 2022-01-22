namespace Common.DataStructures.Graphs.Collections;

using Common.DataStructures.Graphs.Actions;
using Common.DataStructures.Graphs.Contracts;
using Common.DataStructures.Graphs.Predicates;
using Common.ExtensionLibrary;
using System.Collections.Generic;

public class AdjacencyGraph<TKey, TEdge>
    : ICloneable,
        IEdgeListAndIncidenceGraph<TKey, TEdge>,
        INodeAndEdgeListGraph<TKey, TEdge>,
        IMutableEdgeListGraph<TKey, TEdge>,
        IMutableIncidenceGraph<TKey, TEdge>,
        IMutableNodeAndEdgeListGraph<TKey, TEdge>,
        IMutableNodeListGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
    where TKey : IEquatable<TKey>
{
    public static Type EdgeType
        => typeof(TEdge);

    public bool AreEdgesEmpty
            => EdgeCount == 0;

    public bool AreNodesEmpty
        => NodeToEdgesMapping.IsEmpty();

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

    public event EdgeAction<TKey, TEdge>? EdgeRemoved;

    public virtual IEnumerable<TEdge> Edges
        => GetEdges();

    public bool IsDirected
    {
        get;
    }

    public event NodeAction<TKey>? NodeAdded;

    public int NodeCount
            => NodeToEdgesMapping.Count;

    public event NodeAction<TKey>? NodeRemoved;

    public IEnumerable<TKey> Nodes
            => NodeToEdgesMapping.Keys;

    private readonly INodeEdgeDictionary<TKey, TEdge> NodeToEdgesMapping;

    public AdjacencyGraph()
        : this(true)
    {
    }

    public AdjacencyGraph(
        bool isDirected)
        : this(
            isDirected,
            -1)
    {
    }

    public AdjacencyGraph(
        bool isDirected,
        int capacity)
        : this(
            isDirected,
            capacity,
            -1)
    {
    }

    public AdjacencyGraph(
        bool isDirected,
        int capacity,
        int edgeCapacity)
    {
        EdgeCapacity = edgeCapacity;
        IsDirected = isDirected;
        NodeToEdgesMapping = capacity.IsLessThanOrEqualTo(-1)
            ? new NodeEdgeDictionary<TKey, TEdge>()
            : new NodeEdgeDictionary<TKey, TEdge>(capacity);
    }

    public AdjacencyGraph(
        bool isDirected,
        int capacity,
        int edgeCapacity,
        Func<int, INodeEdgeDictionary<TKey, TEdge>> nodeToEdgesMappingFactory)
    {
        if (NodeToEdgesMapping is null)
        {
            throw new ArgumentNullException(nameof(nodeToEdgesMappingFactory));
        }

        IsDirected = isDirected;
        NodeToEdgesMapping = nodeToEdgesMappingFactory(capacity);
        EdgeCapacity = edgeCapacity;
    }

    private AdjacencyGraph(
        INodeEdgeDictionary<TKey, TEdge> nodeEdges,
        int edgeCount,
        int edgeCapacity,
        bool isDirected)
    {
        if (nodeEdges is null)
        {
            throw new ArgumentNullException(nameof(nodeEdges));
        }

        if (edgeCount.IsLessThanZero())
        {
            throw new ArgumentOutOfRangeException(nameof(edgeCount));
        }

        EdgeCapacity = edgeCapacity;
        EdgeCount = edgeCount;
        IsDirected = isDirected;
        NodeToEdgesMapping = nodeEdges;
    }

    public bool AddEdge(
        TEdge edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        if (!NodeToEdgesMapping.ContainsKey(edge.Source))
        {
            return false;
        }

        NodeToEdgesMapping[edge.Source].Add(edge);
        OnEdgeRemoved(edge);

        return true;
    }

    public int AddEdges(
        IEnumerable<TEdge> edges)
        => edges.Count(AddEdge);

    public bool AddNode(
        TKey nodeKey)
    {
        if (ContainsNode(nodeKey))
        {
            return false;
        }

        NodeToEdgesMapping.Add(
            nodeKey,
            EdgeCapacity.IsGreaterThanZero()
            ? new EdgeList<TKey, TEdge>(EdgeCapacity)
            : new EdgeList<TKey, TEdge>());
        OnNodeAdded(nodeKey);

        return true;
    }

    public int AddNodes(
        IEnumerable<TKey> nodeKeys)
        => nodeKeys.Count(AddNode);

    public virtual bool AddNodesAndEdge(
        TEdge edge)
        => AddNode(edge.Source)
            && AddNode(edge.Target)
            && AddEdge(edge);

    public int AddNodesAndEdges(
        IEnumerable<TEdge> edges)
        => edges.Count(AddNodesAndEdge);

    public void Clear()
    {
        NodeToEdgesMapping.Clear();
        EdgeCount = 0;
    }

    public void ClearOutEdges(
        TKey sourceNodeKey)
    {
        IEdgeList<TKey, TEdge>? outEdges = NodeToEdgesMapping[sourceNodeKey];
        int outEdgeCount = outEdges.Count;
        if (EdgeRemoved is not null)
        {
            foreach (TEdge? outEdge in outEdges)
            {
                OnEdgeRemoved(outEdge);
            }
        }

        outEdges.Clear();
        EdgeCount -= outEdgeCount;
    }

    public AdjacencyGraph<TKey, TEdge> Clone()
        => new(
            NodeToEdgesMapping.Clone(),
            EdgeCount,
            EdgeCapacity,
            IsDirected);

    object ICloneable.Clone()
        => Clone();

    public bool ContainsEdge(
            TEdge edge)
        => NodeToEdgesMapping.TryGetValue(
                edge.Source,
                out IEdgeList<TKey, TEdge>? edges)
            && edges is not null
            && edges.Contains(edge);

    public bool ContainsEdge(
        TKey sourceNodeKey,
        TKey targetNodeKey)
    {
        if (TryGetOutEdges(
                sourceNodeKey,
                out IEnumerable<TEdge>? outEdges)
            && outEdges is not null)
        {
            foreach (
                TEdge? outEdge
                in outEdges)
            {
                if (outEdge.Target.Equals(targetNodeKey))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool ContainsNode(
        TKey nodeKey)
        => NodeToEdgesMapping.ContainsKey(nodeKey);

    public TEdge GetOutEdge(
        TKey nodeKey,
        int outEdgeIndex)
        => NodeToEdgesMapping[nodeKey][outEdgeIndex];

    public IEnumerable<TEdge> GetOutEdges(
        TKey nodeKey)
        => NodeToEdgesMapping[nodeKey];

    public bool IsOutEdgesEmpty(
        TKey nodeKey)
        => NodeToEdgesMapping[nodeKey].IsEmpty();

    public virtual void OnEdgeAdded(
        TEdge edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        EdgeAction<TKey, TEdge>? edgeAddedEventHandler = EdgeAdded;
        if (edgeAddedEventHandler is null)
        {
            return;
        }

        edgeAddedEventHandler(edge);
    }

    public virtual void OnEdgeRemoved(
        TEdge edge)
    {
        EdgeAction<TKey, TEdge>? edgeRemovedEventHandler = EdgeRemoved;
        if (edgeRemovedEventHandler is null)
        {
            return;
        }

        edgeRemovedEventHandler(edge);
    }

    public virtual void OnNodeAdded(
        TKey nodeKey)
    {
        if (nodeKey is null)
        {
            throw new ArgumentNullException(nameof(nodeKey));
        }

        NodeAction<TKey>? nodeAddedEventHandler = NodeAdded;
        if (nodeAddedEventHandler is null)
        {
            return;
        }

        nodeAddedEventHandler(nodeKey);
    }

    public virtual void OnNodeRemoved(
        TKey nodeKey)
    {
        if (nodeKey is null)
        {
            throw new ArgumentNullException(nameof(nodeKey));
        }

        NodeAction<TKey>? nodeRemovedEventHandler = NodeRemoved;
        if (nodeRemovedEventHandler is null)
        {
            return;
        }

        nodeRemovedEventHandler(nodeKey);
    }

    public int OutDegree(
        TKey nodeKey)
        => NodeToEdgesMapping[nodeKey].Count;

    public virtual bool RemoveEdge(
        TEdge edge)
    {
        if (NodeToEdgesMapping.TryGetValue(
                edge.Source,
                out IEdgeList<TKey, TEdge>? outEdges)
            && outEdges is not null
            && outEdges.Remove(edge))
        {
            EdgeCount--;
            OnEdgeRemoved(edge);

            return true;
        }

        return false;
    }

    public int RemoveEdge(
        EdgePredicate<TKey, TEdge> predicate)
    {
        var edgeList = new EdgeList<TKey, TEdge>();
        foreach (TEdge? edge in Edges)
        {
            if (predicate(edge))
            {
                edgeList.Add(edge);
            }
        }

        foreach (TEdge edge in edgeList)
        {
            OnEdgeRemoved(edge);
            NodeToEdgesMapping[edge.Source].Remove(edge);
        }

        EdgeCount -= edgeList.Count;
        return edgeList.Count;
    }

    public virtual bool RemoveNode(
        TKey nodeKey)
    {
        if (!NodeToEdgesMapping.ContainsKey(nodeKey))
        {
            return false;
        }

        IEdgeList<TKey, TEdge>? outEdges = NodeToEdgesMapping[nodeKey];
        if (EdgeRemoved is not null)
        {
            foreach (
                TEdge? outEdge in outEdges)
            {
                OnEdgeRemoved(outEdge);
            }

            EdgeCount -= outEdges.Count;
            outEdges.Clear();
        }

        foreach (
            KeyValuePair<TKey, IEdgeList<TKey, TEdge>> keyValuePair
            in NodeToEdgesMapping)
        {
            if (keyValuePair.Key.Equals(nodeKey))
            {
                continue;
            }

            foreach (
                TEdge? edge in keyValuePair.Value.Clone())
            {
                if (edge.Target.Equals(nodeKey))
                {
                    keyValuePair
                        .Value
                        .Remove(edge);
                    OnEdgeRemoved(edge);
                    EdgeCount--;
                }
            }
        }

        if (EdgeCount.IsLessThanZero())
        {
            throw new ApplicationException(
                "Invalid Graph State: Cannot have a negative edge count.");
        }

        NodeToEdgesMapping.Remove(nodeKey);
        OnNodeRemoved(nodeKey);

        return true;
    }

    public int RemoveNodes(
        IEnumerable<TKey> nodeKeys)
        => nodeKeys.Count(RemoveNode);

    public int RemoveNodes(
        NodePredicate<TKey> predicate)
    {
        var nodeList = new NodeList<TKey>();
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

        return nodeList.Count(RemoveNode);
    }

    public int RemoveOutEdge(
        TKey sourceNodeKey,
        EdgePredicate<TKey, TEdge> predicate)
    {
        if (!NodeToEdgesMapping.ContainsKey(sourceNodeKey))
        {
            return 0;
        }

        IEdgeList<TKey, TEdge>? outEdges = NodeToEdgesMapping[sourceNodeKey];
        var edgesToRemove = new EdgeList<TKey, TEdge>(outEdges.Count);
        foreach (
            TEdge? outEdge in outEdges)
        {
            if (outEdge is null)
            {
                continue;
            }

            if (predicate(outEdge))
            {
                edgesToRemove.Add(outEdge);
            }
        }

        foreach (
            TEdge? edgeToRemove in edgesToRemove)
        {
            if (edgesToRemove is null)
            {
                continue;
            }

            outEdges.Remove(edgeToRemove);
            OnEdgeRemoved(edgeToRemove);
        }

        EdgeCount -= edgesToRemove.Count;
        return edgesToRemove.Count;
    }

    public void TrimEdgeExcessMemory()
        => NodeToEdgesMapping.Values
            .ToList()
            .ForEach(
                edgeList =>
                edgeList.TrimExcess());

    public bool TryGetEdge(
        TKey sourceNodeKey,
        TKey targetNodeKey,
        out TEdge? edge)
    {
        if (NodeToEdgesMapping.TryGetValue(
                sourceNodeKey,
                out IEdgeList<TKey, TEdge>? edges)
            && edges is not null
            && !edges.IsEmpty())
        {
            foreach (TEdge? pair in edges)
            {
                if (pair is null)
                {
                    continue;
                }

                if (pair.Target.Equals(targetNodeKey))
                {
                    edge = pair;
                    return true;
                }
            }
        }

        edge = default;
        return false;
    }

    public bool TryGetEdges(
        TKey sourceNodeKey,
        TKey targetNodeKey,
        out IEnumerable<TEdge>? edges)
    {
        if (NodeToEdgesMapping.TryGetValue(
                sourceNodeKey,
                out IEdgeList<TKey, TEdge>? edgeList)
            && edgeList is not null
            && !edgeList.IsEmpty())
        {
            var foundEdges = new List<TEdge>(edgeList.Count);
            foreach (TEdge? edge in edgeList)
            {
                if (edge is null)
                {
                    continue;
                }

                if (edge.Target.Equals(targetNodeKey))
                {
                    foundEdges.Add(edge);
                }
            }

            edges = foundEdges;
            return true;
        }

        edges = null;
        return false;
    }

    public virtual bool TryGetOutEdges(
        TKey nodeKey,
        out IEnumerable<TEdge>? outEdges)
    {
        if (NodeToEdgesMapping.TryGetValue(
                  nodeKey,
                  out IEdgeList<TKey, TEdge>? edges)
              && !edges.IsNullOrEmpty())
        {
            outEdges = edges;
            return true;
        }

        outEdges = null;
        return false;
    }

    protected IEnumerable<TEdge> GetEdges()
    {
        foreach (
            IEdgeList<TKey, TEdge>? edgeList
            in NodeToEdgesMapping.Values)
        {
            foreach (
                TEdge? edge
                in edgeList)
            {
                yield return edge;
            }
        }
    }
}