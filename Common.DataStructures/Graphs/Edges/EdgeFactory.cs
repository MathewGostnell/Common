namespace Common.DataStructures.Graphs.Edges;

using Common.DataStructures.Graphs.Contracts;

public delegate TEdge EdgeFactory<TKey, TEdge>(
    TKey sourceKey,
    TKey targetKey)
    where TEdge : IEdge<TKey>;