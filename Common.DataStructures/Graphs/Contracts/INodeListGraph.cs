namespace Common.DataStructures.Graphs.Contracts;

public interface INodeListGraph<TKey, TEdge>
    : IIncidenceGraph<TKey, TEdge>,
        INodeSet<TKey>
    where TEdge : IEdge<TKey>
{
}