namespace Common.DataStructures.Graphs.Contracts;

public interface IMutableNodeAndEdgeSet<TKey, TEdge>
    : IEdgeListGraph<TKey, TEdge>,
        IMutableEdgeListGraph<TKey, TEdge>,
        IMutableNodeSet<TKey>
    where TEdge : IEdge<TKey>
{
    public bool AddNodesAndEdge(
        TEdge edge);

    public int AddNodesAndEdges(
        IEnumerable<TEdge> edges);
}