namespace Common.DataStructures.Graphs.Contracts
{
    public interface IGraph<TKey, TEdge>
        where TEdge : IEdge<TKey>
    {
        public bool IsDirected
        {
            get;
        }
    }
}
