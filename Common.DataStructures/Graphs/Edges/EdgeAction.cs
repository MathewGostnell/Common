namespace Common.DataStructures.Graphs.Edges;

using Common.DataStructures.Graphs.Contracts;

public delegate void EdgeAction<TKey, TEdge>(
    TEdge edge)
    where TEdge : IEdge<TKey>;