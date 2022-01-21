namespace Common.DataStructures.Contracts
{
    public interface IGraph<TEdge, TKey, TNode> : IGraphStorage<TEdge, TKey, TNode>
        where TEdge : IEdge<TKey>
        where TKey : IEquatable<TKey>
    {
        public ICollection<TEdge> Edges
        {
            get;
        }

        public ICollection<TKey> Nodes
        {
            get;
        }
    }
}
