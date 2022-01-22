namespace Common.DataStructures.Graphs.Contracts
{
    public interface IEdgeList<TKey, TEdge>
        : ICloneable,
            IList<TEdge>
        where TEdge : IEdge<TKey>
    {
        public new IEdgeList<TKey, TEdge> Clone();

        public void TrimExcess();
    }
}