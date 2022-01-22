namespace Common.DataStructures.Graphs.Contracts;

using Common.DataStructures.Graphs.Predicates;

public interface IImplicitUndirectedGraph<TKey, TEdge>
    : IImplicitNodeSet<TKey>,
        IGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public EdgeEqualityComparer<TKey, TEdge> EdgeEqualityComparer
    {
        get;
    }

    public int AdjacentDegree(
        TKey nodeKey);

    public TEdge AdjacentEdge(
        TKey sourceKey,
        int adjacentEdgeIndex);

    public IEnumerable<TEdge> AdjacentEdges(
                TKey nodeKey);

    public bool ContainsEdge(
        TKey sourceKey,
        TKey targetKey);

    public bool IsAdjacentEdgesEmpty(
            TKey nodeKey);

    public bool TryGetEdge(
        TKey sourceKey,
        TKey targetKey,
        out TEdge? edge);
}