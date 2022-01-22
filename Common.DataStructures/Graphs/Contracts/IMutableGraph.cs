namespace Common.DataStructures.Graphs.Contracts
{
    /// <summary>
    /// Defines a <see cref="IGraph&lt;TVertex, TEdge&gt;"/> that can be cleared.
    /// </summary>
    public interface IMutableGraph<TKey, TEdge>
        : IGraph<TKey, TEdge>
        where TEdge : IEdge<TKey>
    {
        /// <summary>
        /// Removes all edges and nodes.
        /// </summary>
        void Clear();
    }
}
