namespace Common.DataStructures.Graphs.Contracts
{
    public interface INodeAndEdgeListGraph<TKey, TEdge>
        : IEdgeListGraph<TKey, TEdge>,
            INodeListGraph<TKey, TEdge>
        where TEdge : IEdge<TKey>
    {

    }
}
