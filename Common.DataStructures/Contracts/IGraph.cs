namespace Common.DataStructures.Contracts
{
    public interface IGraph<TEdge, TKey, TVertex>
        where TEdge : IEdge<TKey>
    {
        public ICollection<TEdge> Edges
        {
            get;
        }

        public IDictionary<TKey, TVertex?> Vertices
        {
            get;
        }


        public bool AddEdge(
            TKey sourceKey,
            TKey targetKey);

        public bool AddVertex(
            TKey vertexKey);

        public bool AddVertices(
            params TKey[] vertexKeys);

        public bool IsAdjacent(
            TKey sourceKey,
            TKey targetKey);

        public ICollection<TKey> GetNeighbors(
            TKey sourceKey);

        public TVertex? GetVertexValue(
            TKey vertexKey);

        public bool RemoveEdge(
            TKey source,
            TKey target);

        public bool RemoveVertex(
            TKey vertexKey);

        public bool SetVertexValue(
            TKey vertexKey,
            TVertex? vertexValue);
    }
}
