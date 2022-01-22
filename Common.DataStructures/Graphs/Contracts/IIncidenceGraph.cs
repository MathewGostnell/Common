namespace Common.DataStructures.Graphs.Contracts;

using System.Collections.Generic;

public interface IIncidenceGraph<TKey, TEdge>
    : IImplicitGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    bool ContainsEdge(
        TKey sourceNodeKey,
        TKey targetNodeKey);

    bool TryGetEdge(
        TKey sourceNodeKey,
        TKey targetNodeKey,
        out TEdge? edge);

    bool TryGetEdges(
        TKey sourceNodeKey,
        TKey targetNodeKey,
        out IEnumerable<TEdge>? edges);
}