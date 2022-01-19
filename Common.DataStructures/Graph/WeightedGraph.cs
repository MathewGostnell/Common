namespace Common.DataStructures.Graph
{
    using Common.DataStructures.Graph.Contracts;
    using Common.ExtensionLibrary;
    using System;

    public class WeightedGraph<TKey, TVertex, TWeight> : BaseGraph<TKey, TVertex, TWeight>, 
            IWeightedGraph<TKey, TVertex, TWeight>
        where TKey : IEquatable<TKey>
    {
        public TWeight? GetEdgeWeight(
            TKey sourceKey,
            TKey targetKey)
            => OutEdges.ContainsKey(sourceKey)
                ? OutEdges[sourceKey].ContainsKey(targetKey)
                    ? OutEdges[sourceKey][targetKey]
                    : default
                : default;

        public IEnumerable<TKey> GetVertices()
            => Verticies.Select(keyValuePair => keyValuePair.Key);

        public bool SetEdgeWeight(
            TKey sourceKey,
            TKey targetKey,
            TWeight? weight)
            => SetEdgeWeight(
                    InEdges,
                    targetKey,
                    sourceKey,
                    weight)
                && SetEdgeWeight(
                    OutEdges,
                    sourceKey,
                    targetKey,
                    weight);

        protected bool SetEdgeWeight(
            IDictionary<TKey, IDictionary<TKey, TWeight?>> dictionary,
            TKey primaryKey,
            TKey secondaryKey,
            TWeight? weight)
        {
            ValidateEdge(
                dictionary,
                primaryKey,
                secondaryKey);

            dictionary[primaryKey].AddOrUpdate(
                secondaryKey,
                weight);
            return true;
        }

        protected void ValidateEdge(
            IDictionary<TKey, IDictionary<TKey, TWeight?>> dictionary,
            TKey sourceKey,
            TKey targetKey)
        {
            if (dictionary.ContainsKey(sourceKey)
                && dictionary[sourceKey].ContainsKey(targetKey))
            {
                return;
            }

            throw new KeyNotFoundException(
                $"An edge between Vertex (key: {sourceKey}) and Vertex (key: {targetKey}) was not found.");
        }
    }
}
