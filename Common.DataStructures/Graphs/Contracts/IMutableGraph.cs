namespace Common.DataStructures.Graphs.Contracts;

public interface IMutableGraph<TKey, TEdge>
    : IGraph<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    void Clear();
}