namespace Common.DataStructures.Contracts
{
    public interface IWeightedGraph<TEdge, TKey, TVertex, TWeight> : IGraph<TEdge, TKey, TVertex>
        where TEdge : IWeightedEdge<TKey, TWeight>
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
