namespace Common.DataStructures.Graphs.Contracts
{
    public interface IEdgeListAndIncidenceGraph<TKey, TEdge>
        : IEdgeListGraph<TKey, TEdge>,
            IIncidenceGraph<TKey, TEdge>
        where TEdge : IEdge<TKey>
    {
    }
}
