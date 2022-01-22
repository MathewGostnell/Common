namespace Common.DataStructures.Graphs.Contracts;

public interface IEdgeListGraph<TKey, TEdge>
    : IEdgeSet<TKey, TEdge>,
        IGraph<TKey, TEdge>,
        INodeSet<TKey>
    where TEdge : IEdge<TKey>
{
}