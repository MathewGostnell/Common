namespace Common.DataStructures.Graph.Contracts
{
    using System;

    internal interface IWeightedGraph<TKey, TVertex, TWeight> 
        : IGraph<TKey, TVertex>
        where TKey : IEquatable<TKey>
    {
        public TWeight GetEdgeWeight(
            TKey sourceKey,
            TKey targetKey);

        public void SetEdgeWeight(
            TKey sourceKey,
            TKey targetKey,
            TWeight weight);
    }
}
