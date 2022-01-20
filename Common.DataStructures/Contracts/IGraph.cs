namespace Common.DataStructures.Contracts
{
    public interface IGraph<TKey, TVertex>
    {
        public ICollection<IEdge<TVertex>> Edges
        {
            get;
        }

        public ICollection<TKey> Vertices
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
