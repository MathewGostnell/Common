namespace Common.DataStructures.Graph.Contracts
{
    public interface IGraph<TKey, TVertex>
        where TKey : IEquatable<TKey>
    {
        public bool AddEdge(
            TKey sourceKey,
            TKey targetKey);

        public bool AddVertex(
            TKey vertexKey);

        public IList<KeyValuePair<TKey, TVertex?>> GetNeighbors(
            TKey sourceKey);

        public TVertex? GetVertexValue(
            TKey vertexKey);

        public bool IsAdjacent(
            TKey sourceKey,
            TKey targetKey);

        public bool RemoveEdge(
            TKey sourceKey,
            TKey targetKey);

        public bool RemoveVertex(
            TKey vertexKey);

        public bool SetVertexValue(
            TKey vertexKey,
            TVertex? vertexValue);
    }
}
