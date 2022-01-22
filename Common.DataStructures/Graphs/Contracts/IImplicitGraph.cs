namespace Common.DataStructures.Graphs.Contracts;

public interface IImplicitGraph<TKey, TEdge>
    : IGraph<TKey, TEdge>,
        IImplicitNodeSet<TKey>
    where TEdge : IEdge<TKey>
{
    public TEdge GetOutEdge(
        TKey nodeKey,
        int outEdgeIndex);

    public IEnumerable<TEdge> GetOutEdges(
        TKey nodeKey);

    bool IsOutEdgesEmpty(
        TKey nodeKey);

    int OutDegree(
        TKey nodeKey);

    bool TryGetOutEdges(
        TKey nodeKey,
        out IEnumerable<TEdge>? outEdges);
}