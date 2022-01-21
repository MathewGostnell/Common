namespace Common.DataStructures.Contracts
{
    using System;

    public interface IWeightedGraphStorage<TEdge, TKey, TNode, TWeight> : IGraphStorage<TEdge, TKey, TNode>
        where TEdge : IWeightedEdge<TKey, TWeight>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
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
