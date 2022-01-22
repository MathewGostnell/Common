namespace Common.DataStructures.Graphs.Edges;

using Common.DataStructures.Graphs.Contracts;

public class EdgeEventArgs<TKey, TEdge> : EventArgs
    where TEdge : IEdge<TKey>
{
    public TEdge Edge
    {
        get;
    }

    public EdgeEventArgs(
        TEdge edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        Edge = edge;
    }
}