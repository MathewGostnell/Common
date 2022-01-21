namespace Common.DataStructures.Contracts
{
    public interface IGraph<TKey, TNode>
        where TKey : IEquatable<TKey>
    {
        public ICollection<IEdge<TKey>> Edges
        {
            get;
        }

        public ICollection<TKey> Nodes
        {
            get;
        }
    }
}
