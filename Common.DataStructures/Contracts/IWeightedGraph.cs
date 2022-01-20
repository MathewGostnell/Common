namespace Common.DataStructures.Contracts
{
    using System.Collections.Generic;

    public interface IWeightedGraph<TKey, TVertex, TWeight> : IGraph<TKey, TVertex>
    {
        public new ICollection<IWeightedEdge<TVertex, TWeight>> Edges
        {
            get;
        }

        public TWeight GetEdgeWeight(
            TKey sourceKey,
            TKey targetKey);
    }
}
