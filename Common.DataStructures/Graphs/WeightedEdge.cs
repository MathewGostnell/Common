namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Contracts;
    using System;

    public class WeightedEdge<TKey, TWeight> : Edge<TKey>, IWeightedEdge<TKey, TWeight>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
    {
        public WeightedEdge(
            TKey sourceKey,
            TKey targetKey,
            TWeight? weight = default)
            : base(
                sourceKey,
                targetKey)
        {
            Weight = weight;
        }


        public TWeight? Weight
        {
            get;
        }
    }
}
