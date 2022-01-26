namespace Common.DataStructures.Graphs.Contracts;

public interface IEdgeFactory<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public TEdge CreateEdge(
        TKey source,
        TKey target);
}