namespace Common.DataStructures.Graph
{
    using Common.DataStructures.Graph.Contracts;
    using System;
    using System.Collections.Generic;

    public abstract class BaseGraph<TKey, TVertex, TWeight> : IGraph<TKey, TVertex>
        where TKey : IEquatable<TKey>
    {
        public BaseGraph()
        {
            InEdges = new Dictionary<TKey, IDictionary<TKey, TWeight?>>();
            OutEdges = new Dictionary<TKey, IDictionary<TKey, TWeight?>>();
            Verticies = new Dictionary<TKey, TVertex?>();
        }

        protected IDictionary<TKey, IDictionary<TKey, TWeight?>> InEdges
        {
            get;
        }

        protected IDictionary<TKey, IDictionary<TKey, TWeight?>> OutEdges
        {
            get;
        }

        protected IDictionary<TKey, TVertex?> Verticies
        {
            get;
        }

        protected bool AddEdge(
            IDictionary<TKey, IDictionary<TKey, TWeight?>> dictionary,
            TKey primaryKey,
            TKey secondaryKey)
        {
            if (dictionary.ContainsKey(primaryKey))
            {
                if (dictionary[primaryKey].ContainsKey(secondaryKey))
                {
                    return false;
                }

                dictionary[primaryKey].Add(
                    secondaryKey,
                    default);
                return true;
            }

            dictionary.Add(
                primaryKey,
                new Dictionary<TKey, TWeight?>
                {
                    { secondaryKey, default }
                });
            return true;
        }

        public bool AddEdge(
            TKey sourceKey, 
            TKey targetKey)
        {
            ValidateVertexKey(sourceKey);
            ValidateVertexKey(targetKey);
            
            return AddInEdge(
                    sourceKey,
                    targetKey)
                && AddOutEdge(
                    sourceKey,
                    targetKey);
        }

        protected bool AddInEdge(
            TKey sourceKey,
            TKey targetKey)
            => AddEdge(
                InEdges,
                targetKey,
                sourceKey);

        protected bool AddOutEdge(
            TKey sourceKey,
            TKey targetKey)
            => AddEdge(
                OutEdges,
                sourceKey,
                targetKey);

        public bool AddVertex(
            TKey vertexKey)
        {
            if (Verticies.ContainsKey(vertexKey))
            {
                return false;
            }

            Verticies.Add(
                vertexKey, 
                default);
            return true;
        }

        public IList<KeyValuePair<TKey, TVertex?>> GetNeighbors(
            TKey sourceKey)
        {
            IEnumerable<TKey> neighbors = new List<TKey>();

            if (InEdges.ContainsKey(sourceKey))
            {
                neighbors = neighbors.Union(
                    InEdges[sourceKey].Select(
                        dictionary =>
                        dictionary.Key));
            }

            if (OutEdges.ContainsKey(sourceKey))
            {
                neighbors = neighbors.Union(
                    OutEdges[sourceKey].Select(
                        dictionary =>
                        dictionary.Key));
            }

            return neighbors
                .Select(
                    vertexKey =>
                    new KeyValuePair<TKey, TVertex?>(
                        vertexKey,
                        GetVertexValue(vertexKey)))
                .ToList();
        }

        public TVertex? GetVertexValue(
            TKey vertexKey)
            => Verticies.ContainsKey(vertexKey)
                ? Verticies[vertexKey]
                : default;

        public bool IsAdjacent(
            TKey sourceKey, 
            TKey targetKey)
            => OutEdges.ContainsKey(sourceKey)
                && OutEdges[sourceKey].ContainsKey(targetKey);

        public bool RemoveEdge(
            TKey sourceKey, 
            TKey targetKey)
            => RemoveInEdge(
                    sourceKey,
                    targetKey)
                && RemoveOutEdge(
                    sourceKey,
                    targetKey);

        protected bool RemoveInEdge(
            TKey sourceKey,
            TKey targetKey)
        {
            if (InEdges.ContainsKey(targetKey))
            {
                return InEdges[targetKey].Remove(sourceKey);
            }

            return false;
        }

        protected bool RemoveOutEdge(
            TKey sourceKey,
            TKey targetKey)
        {
            if (OutEdges.ContainsKey(sourceKey))
            {
                return OutEdges[sourceKey].Remove(targetKey);
            }

            return false;
        }

        public bool RemoveVertex(
            TKey vertexKey)
            => InEdges.Remove(vertexKey)
                || OutEdges.Remove(vertexKey)
                || Verticies.Remove(vertexKey);

        public bool SetVertexValue(
            TKey vertexKey, 
            TVertex? vertexValue)
        {
            ValidateVertexKey(vertexKey);

            Verticies[vertexKey] = vertexValue;
            return true;
        }

        protected void ValidateVertexKey(
            TKey vertexKey)
        {
            if (Verticies.ContainsKey(vertexKey))
            {
                return;
            }

            throw new KeyNotFoundException(
                $"Vertex (key: {vertexKey}) was not found.");
        }
    }
}
