namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Graphs.Contracts;

    public delegate bool EdgePredicate<TKey, TEdge>(
        TEdge edge)
        where TEdge : IEdge<TKey>;
}
