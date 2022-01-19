namespace Common.DataStructures.Graph.Contracts
{
    using System;

    public interface IWeightedGraph<TKey, TVertex, TWeight> 
        : IGraph<TKey, TVertex>
        where TKey : IEquatable<TKey>
    {
        public TWeight? GetEdgeWeight(
            TKey sourceKey,
            TKey targetKey);

        public bool SetEdgeWeight(
            TKey sourceKey,
            TKey targetKey,
            TWeight? weight);
    }
}
