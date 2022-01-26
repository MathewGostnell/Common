namespace Common.DataStructures.Graphs.Contracts;

public interface IEdgeSet<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public IEnumerable<TEdge> Edges
    {
        get;
    }

    public bool ContainsEdge(
        TKey sourceKey,
        TKey targetKey);

    public bool ContainsEdge(
        TEdge edge);
}