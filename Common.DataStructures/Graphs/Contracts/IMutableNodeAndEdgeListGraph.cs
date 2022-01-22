namespace Common.DataStructures.Graphs.Contracts;

public interface IMutableNodeAndEdgeListGraph<TKey, TEdge>
    : IMutableNodeListGraph<TKey, TEdge>,
        IMutableEdgeListGraph<TKey, TEdge>,
        IMutableNodeAndEdgeSet<TKey, TEdge>,
        INodeAndEdgeListGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
}