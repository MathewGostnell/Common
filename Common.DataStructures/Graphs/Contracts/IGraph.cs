namespace Common.DataStructures.Graphs.Contracts;
public interface IGraph<TKey, TEdge>
    : IEdgeSet<TKey, TEdge>,
        INodeSet<TKey>
    where TEdge : IEdge<TKey>
{
    public bool IsDirected
    {
        get;
    }
}