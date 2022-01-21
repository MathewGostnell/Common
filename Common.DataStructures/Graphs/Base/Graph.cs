namespace Common.DataStructures.Graphs.Base
{
    using Common.DataStructures.Contracts;
    using System.Collections.Generic;

    public abstract class Graph<TEdge, TKey, TVertex> : IGraph<TEdge, TKey, TVertex>
        where TEdge : IEdge<TKey>
        where TKey : IEquatable<TKey>
    {
        public Graph()
        {
            Edges = new List<TEdge>();
            Vertices = new Dictionary<TKey, TVertex?>();
        }

        public ICollection<TEdge> Edges
        {
            get;
        }

        public IDictionary<TKey, TVertex?> Vertices
        {
            get;
        }

        public abstract bool AddEdge(
            TKey sourceKey,
            TKey targetKey);

        public abstract bool AddVertex(
            TKey vertexKey);

        public bool AddVertices(
            params TKey[] vertexKeys)
        {
            var addedVertices = vertexKeys
                .ToList()
                .Select(
                    vertexKey =>
                    AddVertex(vertexKey));
            if (!addedVertices.Any())
            {
                return false;
            }
                  
            return addedVertices.Aggregate(
                (current, next) =>
                current &= next);
        }

        public abstract ICollection<TKey> GetNeighbors(
            TKey sourceKey);

        public abstract TVertex? GetVertexValue(
            TKey vertexKey);

        public abstract bool IsAdjacent(
            TKey sourceKey,
            TKey targetKey);

        public abstract bool RemoveEdge(
            TKey source,
            TKey target);

        public abstract bool RemoveVertex(
            TKey vertexKey);

        public abstract bool SetVertexValue(
            TKey vertexKey,
            TVertex? vertexValue);
    }
}
