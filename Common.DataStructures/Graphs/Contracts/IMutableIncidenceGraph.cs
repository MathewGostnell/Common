namespace Common.DataStructures.Graphs.Contracts;

using Common.DataStructures.Graphs.Predicates;

public interface IMutableIncidenceGraph<TKey, TEdge>
    : IMutableGraph<TKey, TEdge>,
        IIncidenceGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    void ClearOutEdges(
        TKey sourceNodeKey);

    int RemoveOutEdge(
        TKey sourceNodeKey,
        EdgePredicate<TKey, TEdge> predicate);

    void TrimEdgeExcessMemory();
}