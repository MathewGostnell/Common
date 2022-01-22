namespace Common.DataStructures.Graphs.Predicates;

using Common.DataStructures.Graphs.Contracts;

public delegate bool EdgeEqualityComparer<TKey, TEdge>(
    TEdge edge,
    TKey sourceKey,
    TKey targetKey)
    where TEdge : IEdge<TKey>;