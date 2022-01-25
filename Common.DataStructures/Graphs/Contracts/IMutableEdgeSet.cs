namespace Common.DataStructures.Graphs.Contracts;

public interface IMutableEdgeSet<TKey, TEdge>
    : IEdgeSet<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public bool AddEdge(
        TKey sourceKey,
        TKey targetKey);

    public int AddEdges(
        IEnumerable<TEdge>? edges);

    public bool AreAdjacent(
        TKey sourceKey,
        TKey targetKey);

    public void Clear();

    public IEnumerable<TEdge> GetNeighbors(
        TKey sourceKey);

    public bool RemoveEdge(
        TKey sourceKey,
        TKey targetKey);

    public int RemoveEdges(
        IEnumerable<TEdge> edges);
}