namespace Common.DataStructures.Graphs.Contracts;

using Common.DataStructures.Graphs.Edges;
using Common.DataStructures.Graphs.Nodes;

public interface IMutableGraph<TKey, TEdge>
    : IGraph<TKey, TEdge>,
        IMutableEdgeSet<TKey, TEdge>,
        IMutableNodeSet<TKey>
    where TEdge : IEdge<TKey>
{
    public event EdgeAction<TKey, TEdge>? EdgeAdded;

    public event EdgeAction<TKey, TEdge>? EdgeRemoved;

    public event NodeAction<TKey>? NodeAdded;

    public event NodeAction<TKey>? NodeRemoved;

    public void OnEdgeAdded(
        TEdge edge);

    public void OnEdgeRemoved(
        TEdge edge);

    public void OnNodeAdded(
        TKey key);

    public void OnNodeRemoved(
        TKey key);
}