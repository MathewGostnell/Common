namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Contracts;
    using System;

    public class WeightedEdge<TKey, TWeight> : Edge<TKey>, IWeightedEdge<TKey, TWeight>
        where TWeight : IComparable<TWeight>
    {
        public WeightedEdge(
            TKey source,
            TKey target,
            TWeight weight)
            : base(
                source,
                target)
        {
            Weight = weight;
        }

        public TWeight Weight
        {
            get;
        }
    }
}
