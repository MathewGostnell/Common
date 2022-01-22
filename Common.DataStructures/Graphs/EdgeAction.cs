namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Graphs.Contracts;

    public delegate void EdgeAction<TKey, TEdge>(
        TEdge edge)
        where TEdge : IEdge<TKey>;
}
