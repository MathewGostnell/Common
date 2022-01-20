namespace Common.DataStructures.Graphs.Base
{
    using Common.DataStructures.Contracts;
    using System.Collections.Generic;

    public abstract class Graph<TKey, TVertex> : IGraph<TKey, TVertex>
    {
        public Graph()
        {
            Edges = new List<IEdge<TVertex>>();
            Vertices = new List<TKey>();
        }

        public ICollection<IEdge<TVertex>> Edges
        {
            get;
        }

        public ICollection<TKey> Vertices
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
