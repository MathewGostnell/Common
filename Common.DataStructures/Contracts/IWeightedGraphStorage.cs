namespace Common.DataStructures.Contracts
{
    using System;

    public interface IWeightedGraphStorage<TKey, TNode, TWeight> : IGraphStorage<IWeightedEdge<TKey, TWeight>, TKey, TNode>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
    {
        public TWeight GetEdgeWeight(
            TKey sourceKey,
            TKey targetKey);

        public bool SetEdgeWeight(
            TKey sourceKey,
            TKey targetKey,
            TWeight weight);
    }
}
