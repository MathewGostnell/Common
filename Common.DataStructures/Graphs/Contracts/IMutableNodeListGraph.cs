namespace Common.DataStructures.Graphs.Contracts
{
    public interface IMutableNodeListGraph<TKey, TEdge>
        : IEdgeListGraph<TKey, TEdge>
        , IMutableNodeSet<TKey>
        , IMutableEdgeListGraph<TKey, TEdge>
        where TEdge : IEdge<TKey>
    {
    }
}
