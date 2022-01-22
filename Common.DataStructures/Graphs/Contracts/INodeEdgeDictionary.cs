namespace Common.DataStructures.Graphs.Contracts
{
    using System.Runtime.Serialization;

    public interface INodeEdgeDictionary<TKey, TEdge>
        : ICloneable,
            IDictionary<TKey, IEdgeList<TKey, TEdge>>,
            ISerializable
        where TEdge : IEdge<TKey>
    {
        public new INodeEdgeDictionary<TKey, TEdge> Clone();
    }
}
