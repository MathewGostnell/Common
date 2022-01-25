namespace Common.DataStructures.Graphs.Edges;

using Common.DataStructures.Graphs.Contracts;

public class EdgeFactory<TKey, TEdge> : IEdgeFactory<TKey, TEdge>
    where TEdge : IEdge<TKey>
{
    public TEdge CreateEdge(
        TKey source,
        TKey target)
    {
        Type edgeType = typeof(TEdge);
        var edge = (TEdge?)Activator.CreateInstance(
            edgeType,
            source,
            target);
        return edge is null
            ? throw new ApplicationException($"Failed to create {edgeType.Name} instance.")
            : edge;
    }
}