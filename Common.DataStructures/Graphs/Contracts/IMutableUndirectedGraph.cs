namespace Common.DataStructures.Graphs.Contracts;

using Common.DataStructures.Graphs.Predicates;

public interface IMutableUndirectedGraph<TKey, TEdge>
    : IMutableEdgeListGraph<TKey, TEdge>,
        IMutableNodeAndEdgeSet<TKey, TEdge>,
        IMutableNodeSet<TKey>,
        IUndirectedGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public void ClearAdjacentEdges(
        TKey nodeKey);

    public int RemoveAdjacentEdge(
            TKey nodeKey,
        EdgePredicate<TKey, TEdge> predicate);
}