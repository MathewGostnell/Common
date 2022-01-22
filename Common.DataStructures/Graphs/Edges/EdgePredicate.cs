namespace Common.DataStructures.Graphs.Edges;

using Common.DataStructures.Graphs.Contracts;

public delegate bool EdgePredicate<TKey, TEdge>(
    TEdge edge)
    where TEdge : IEdge<TKey>;