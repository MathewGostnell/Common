namespace Common.DataStructures.Graphs.Collections;

using Common.DataStructures.Graphs.Contracts;
using Common.ExtensionLibrary;
using System.Collections.Generic;

public sealed class AdjacencyArrayGraph<TKey, TEdge>
    : INodeAndEdgeListGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
    where TKey : IEquatable<TKey>
{
    public bool AreEdgesEmpty
        => !Edges.Any();

    public bool AreNodesEmpty
        => !Nodes.Any();

    public int EdgeCount
    {
        get;
        private set;
    }

    public IEnumerable<TEdge> Edges
        => GetEdges();

    public bool IsDirected
    {
        get;
        private set;
    }

    public int NodeCount
        => Nodes.Count();

    public IEnumerable<TKey> Nodes
        => NodeToEdgesMapping.Keys;

    public IDictionary<TKey, TEdge[]> NodeToEdgesMapping
    {
        get;
        private set;
    }

    public AdjacencyArrayGraph(
        IDictionary<TKey, TEdge[]> nodeOutEdges,
        int edgeCount)
    {
        if (nodeOutEdges is null)
        {
            throw new ArgumentNullException(nameof(nodeOutEdges));
        }

        if (edgeCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(edgeCount));
        }

        int summedCount = Enumerable.Sum(
            nodeOutEdges,
            keyValuePair =>
            (keyValuePair.Value is null)
            ? 0
            : keyValuePair.Value.Length);
        if (edgeCount != summedCount)
        {
            throw new ArgumentOutOfRangeException(nameof(edgeCount));
        }

        EdgeCount = edgeCount;
        NodeToEdgesMapping = nodeOutEdges;
    }

    public AdjacencyArrayGraph(
        INodeAndEdgeListGraph<TKey, TEdge> visitedGraph)
    {
        if (visitedGraph is null)
        {
            throw new ArgumentNullException(nameof(visitedGraph));
        }

        NodeToEdgesMapping = new Dictionary<TKey, TEdge[]>(
            visitedGraph.NodeCount);
        EdgeCount = visitedGraph.EdgeCount;
        foreach (
            TKey? edgeTarget in visitedGraph.Nodes)
        {
            var outEdges = new List<TEdge>(visitedGraph.GetOutEdges(edgeTarget));
            NodeToEdgesMapping.Add(
                edgeTarget,
                outEdges.ToArray());
        }
    }

    public bool ContainsEdge(
        TKey sourceNodeKey,
        TKey targetNodeKey)
        => TryGetEdge(
            sourceNodeKey,
            targetNodeKey,
            out _);

    public bool ContainsNode(
        TKey nodeKey)
        => NodeToEdgesMapping.ContainsKey(nodeKey);

    public TEdge GetOutEdge(
        TKey nodeKey,
        int outEdgeIndex)
        => NodeToEdgesMapping[nodeKey][outEdgeIndex];

    public IEnumerable<TEdge> GetOutEdges(
        TKey nodeKey)
       => NodeToEdgesMapping.TryGetValue(
                nodeKey,
                out TEdge[]? edges)
            && edges is not null
            ? edges
            : Enumerable.Empty<TEdge>();

    public bool IsOutEdgesEmpty(
        TKey nodeKey)
        => OutDegree(nodeKey) == 0;

    public int OutDegree(
        TKey nodeKey)
        => NodeToEdgesMapping
            .TryGetValue(
                nodeKey,
                out TEdge[]? edges)
            && edges is not null
            ? edges.Length
            : 0;

    public bool TryGetEdge(
        TKey sourceNodeKey,
        TKey targetNodeKey,
        out TEdge? edge)
    {
        if (NodeToEdgesMapping.TryGetValue(
                sourceNodeKey,
                out TEdge[]? foundEdges)
            && foundEdges is not null)
        {
            for (int edgeIndex = 0;
                edgeIndex.IsLessThan(foundEdges.Length);
                edgeIndex++)
            {
                if (foundEdges[edgeIndex].Target.Equals(targetNodeKey))
                {
                    edge = foundEdges[edgeIndex];
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
                out TEdge[]? mappedEdges)
            && mappedEdges is not null)
        {
            IList<TEdge>? foundEdges = null;
            for (int edgeIndex = 0;
                edgeIndex.IsLessThan(mappedEdges.Length);
                edgeIndex++)
            {
                if (mappedEdges[edgeIndex].Target.Equals(targetNodeKey))
                {
                    if (foundEdges is null)
                    {
                        foundEdges = new List<TEdge>(mappedEdges.Length - edgeIndex);
                    }

                    foundEdges.Add(mappedEdges[edgeIndex]);
                }
            }

            edges = foundEdges;
            return edges != null;
        }

        edges = null;
        return false;
    }

    public bool TryGetOutEdges(
        TKey nodeKey,
        out IEnumerable<TEdge>? outEdges)
    {
        if (NodeToEdgesMapping.TryGetValue(
                nodeKey,
                out TEdge[]? edges)
            && edges is not null)
        {
            outEdges = edges;
            return true;
        }

        outEdges = null;
        return false;
    }

    private IEnumerable<TEdge> GetEdges()
    {
        foreach (
            TEdge[]? edges in NodeToEdgesMapping.Values)
        {
            if (edges is null)
            {
                continue;
            }

            for (int edgeIndex = 0;
                edgeIndex.IsLessThan(edges.Length);
                edgeIndex++)
            {
                yield return edges[edgeIndex];
            }
        }
    }
}