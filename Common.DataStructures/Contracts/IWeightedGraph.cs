namespace Common.DataStructures.Contracts
{
    using Common.DataStructures.Graphs;

    public interface IWeightedGraph<TKey, TNode, TWeight> : IWeightedGraphStorage<TKey, TNode, TWeight>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
    {
        public ICollection<IWeightedEdge<TKey, TWeight>> Edges
        {
            get;
        }

        public ICollection<TKey> Nodes
        {
            get;
        }
    }
}
