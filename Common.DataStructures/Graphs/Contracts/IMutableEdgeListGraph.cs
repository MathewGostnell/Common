namespace Common.DataStructures.Graphs.Contracts;

using Common.DataStructures.Graphs.Actions;
using Common.DataStructures.Graphs.Predicates;

public interface IMutableEdgeListGraph<TKey, TEdge>
    : IMutableGraph<TKey, TEdge>,
        IEdgeListGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public bool AddEdge(
        TEdge edge);

    public int AddEdges(
        IEnumerable<TEdge> edges);

    public event EdgeAction<TKey, TEdge> EdgeAdded;

    public event EdgeAction<TKey, TEdge> EdgeRemoved;

    public bool RemoveEdge(
        TEdge edge);

    public int RemoveEdge(
        EdgePredicate<TKey, TEdge> predicate);
}