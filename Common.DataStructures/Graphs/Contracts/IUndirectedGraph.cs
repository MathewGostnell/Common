namespace Common.DataStructures.Graphs.Contracts;

public interface IUndirectedGraph<TKey, TEdge>
    : IEdgeListGraph<TKey, TEdge>,
        IGraph<TKey, TEdge>,
        IImplicitUndirectedGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public IEnumerable<TKey> AdjacentVertices(
        TKey nodeKey);

    public int RemoveEdges(
        IEnumerable<TEdge> edges);

    public void OnEdgeRemoved(
        TEdge edge);

    public void OnNodeRemoved(
        TKey nodeKey);
}