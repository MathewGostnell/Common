namespace Common.DataStructures.Graph
{
    using Common.DataStructures.Graph.Contracts;
    using Common.ExtensionLibrary;
    using System;
    using System.Collections.Generic;

    public class BaseGraph<TKey, TVertex> : IGraph<TKey, TVertex>
        where TKey : IEquatable<TKey>
    {
        public BaseGraph()
        {
            InEdges = new Dictionary<TKey, IList<TKey>>();
            OutEdges = new Dictionary<TKey, IList<TKey>>();
            Verticies = new Dictionary<TKey, TVertex?>();
        }

        protected IDictionary<TKey, IList<TKey>> InEdges
        {
            get;
        }

        protected IDictionary<TKey, IList<TKey>> OutEdges
        {
            get;
        }

        protected IDictionary<TKey, TVertex?> Verticies
        {
            get;
        }

        public bool AddEdge(
            TKey sourceKey, 
            TKey targetKey)
        {
            ValidateVertexKey(sourceKey);
            ValidateVertexKey(targetKey);

            var addedEdge = true;
            addedEdge &= AddInEdge(
                sourceKey,
                targetKey);
            addedEdge &= AddOutEdge(
                sourceKey,
                targetKey);

            return addedEdge;
        }

        protected bool AddInEdge(
            TKey sourceKey,
            TKey targetKey)
        {
            if (InEdges.ContainsKey(targetKey))
            {
                if (InEdges[targetKey].Contains(sourceKey))
                {
                    return false;
                }

                InEdges[targetKey].Add(sourceKey);
                return false;
            }

            InEdges.Add(
                targetKey,
                new List<TKey>
                {
                    sourceKey
                });
            return true;
        }

        protected bool AddOutEdge(
            TKey sourceKey,
            TKey targetKey)
        {
            if (OutEdges.ContainsKey(sourceKey))
            {
                if (OutEdges[sourceKey].Contains(targetKey))
                {
                    return false;
                }

                var neighbors = OutEdges[sourceKey];
                neighbors.Add(targetKey);
                OutEdges[sourceKey] = neighbors;
                return false;
            }

            OutEdges.Add(
                sourceKey,
                new List<TKey>
                {
                    targetKey
                });
            return true;
        }

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
            var inEdges = InEdges.ContainsKey(sourceKey)
                ? InEdges[sourceKey]
                : new List<TKey>();
            var outEdges = OutEdges.ContainsKey(sourceKey)
                ? OutEdges[sourceKey]
                : new List<TKey>();

            return inEdges.Union(outEdges)
                .Distinct()
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
                && OutEdges[sourceKey].Contains(targetKey);

        public bool RemoveEdge(
            TKey sourceKey, 
            TKey targetKey)
        {
            ValidateVertexKey(sourceKey);
            ValidateVertexKey(targetKey);

            return RemoveInEdge(
                    sourceKey,
                    targetKey)
                && RemoveOutEdge(
                    sourceKey,
                    targetKey);
        }

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
        {
            return Verticies.Remove(vertexKey);
        }

        public bool SetVertexValue(
            TKey vertexKey, 
            TVertex? vertexValue)
        {
            ValidateVertexKey(vertexKey);

            return Verticies.AddOrUpdate(
                vertexKey,
                vertexValue);
        }

        protected void ValidateVertexKey(
            TKey vertexKey)
        {
            if (Verticies.ContainsKey(vertexKey))
            {
                return;
            }

            throw new ArgumentOutOfRangeException(
                nameof(vertexKey),
                $"A vertex with a key of {vertexKey} does not exist.");
        }
    }
}
