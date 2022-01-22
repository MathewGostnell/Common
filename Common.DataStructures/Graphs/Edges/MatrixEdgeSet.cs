namespace Common.DataStructures.Graphs.Edges;

using Common.DataStructures.Graphs.Contracts;
using System.Collections.Generic;

public class MatrixEdgeSet<TKey, TEdge>
    : EdgeSet<TKey, TEdge>,
        IMutableEdgeSet<TKey, TEdge>
    where TEdge : IEdge<TKey>
    where TKey : IEquatable<TKey>
{
    public override IEnumerable<TEdge> Edges
        => GetEdges();

    protected IDictionary<TKey, IDictionary<TKey, bool>> AdjacencyMatrix
    {
        get;
    }

    protected EdgeFactory<TKey, TEdge> EdgeFactory
    {
        get;
    }

    public MatrixEdgeSet(
        EdgeFactory<TKey, TEdge> edgeFactory)
    {
        if (edgeFactory is null)
        {
            throw new ArgumentNullException(nameof(edgeFactory));
        }

        AdjacencyMatrix = new Dictionary<TKey, IDictionary<TKey, bool>>();
        EdgeFactory = edgeFactory;
    }

    public bool AddEdge(
        TKey sourceKey,
        TKey targetKey)
    {
        if (ContainsEdge(
            sourceKey,
            targetKey))
        {
            return false;
        }

        if (!AdjacencyMatrix.ContainsKey(sourceKey))
        {
            AdjacencyMatrix.Add(
                sourceKey,
                new Dictionary<TKey, bool>
                {
                    {targetKey, false}
                });
            return true;
        }

        AdjacencyMatrix[sourceKey].Add(
            targetKey,
            true);
        return true;
    }

    public int AddEdges(
        IEnumerable<TEdge> edges)
        => edges.Count(
            edge =>
            AddEdge(
                edge.SourceKey,
                edge.TargetKey));

    public bool AreAdjacent(
        TKey sourceKey,
        TKey targetKey)
        => ContainsEdge(
                sourceKey,
                targetKey)
            && AdjacencyMatrix[sourceKey][targetKey];

    public void Clear()
        => AdjacencyMatrix.Clear();

    public override bool ContainsEdge(
        TKey sourceKey,
        TKey targetKey)
        => AdjacencyMatrix.ContainsKey(sourceKey)
            && AdjacencyMatrix[sourceKey].ContainsKey(targetKey);

    public override bool ContainsEdge(TEdge edge)
        => ContainsEdge(
            edge.SourceKey,
            edge.TargetKey);

    public IEnumerable<TEdge> GetNeighbors(
        TKey sourceKey)
    {
        var neighborList = new List<TEdge>();
        if (!AdjacencyMatrix.ContainsKey(sourceKey))
        {
            return neighborList;
        }

        foreach (
            KeyValuePair<TKey, bool> keyValuePair
            in AdjacencyMatrix[sourceKey])
        {
            if (keyValuePair.Value)
            {
                neighborList.Add(
                    EdgeFactory(
                        sourceKey,
                        keyValuePair.Key));
            }
        }

        return neighborList;
    }

    public bool RemoveEdge(
        TKey sourceKey,
        TKey targetKey)
    {
        if (ContainsEdge(sourceKey, targetKey))
        {
            AdjacencyMatrix[sourceKey][targetKey] = false;
            return true;
        }

        return false;
    }

    public int RemoveEdges(IEnumerable<TEdge> edges)
        => edges.Count(
            edge => RemoveEdge(
                edge.SourceKey,
                edge.TargetKey));

    protected IEnumerable<TEdge> GetEdges()
    {
        var edgeList = new List<TEdge>();
        foreach (
            KeyValuePair<TKey, IDictionary<TKey, bool>> keyValuePair
            in AdjacencyMatrix)
        {
            foreach (
                KeyValuePair<TKey, bool> edgeKeyValuePair
                in keyValuePair.Value)
            {
                if (edgeKeyValuePair.Value)
                {
                    edgeList.Add(
                        EdgeFactory(
                            keyValuePair.Key,
                            edgeKeyValuePair.Key));
                }
            }
        }

        return edgeList;
    }
}