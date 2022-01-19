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

        protected bool AddEdge(
            IDictionary<TKey, IList<TKey>> dictionary,
            TKey primaryKey,
            TKey secondaryKey)
        {
            if (dictionary.ContainsKey(primaryKey))
            {
                if (dictionary[primaryKey].Contains(secondaryKey))
                {
                    return false;
                }

                var neighbors = dictionary[primaryKey];
                neighbors.Add(secondaryKey);
                dictionary[primaryKey] = neighbors;
                return true;
            }

            dictionary.Add(
                primaryKey,
                new List<TKey>
                {
                    secondaryKey
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

            throw new ArgumentOutOfRangeException(
                nameof(vertexKey),
                $"A vertex with a key of {vertexKey} does not exist.");
        }
    }
}
