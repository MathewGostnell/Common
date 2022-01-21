namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Contracts;
    using System;
    using System.Linq;

    public class WeightedGraph<TEdge, TKey, TVertex, TWeight> 
        : DirectedGraph<TEdge, TKey, TVertex>,
            IWeightedGraph<TEdge, TKey, TVertex, TWeight>
        where TEdge : class, IWeightedEdge<TKey, TWeight>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
    {
        public TWeight GetEdgeWeight(
            TKey sourceKey, 
            TKey targetKey)
            => AdjacencyList[sourceKey]
                .First(
                    node => 
                    node.Target
                        .Equals(targetKey))
                .Weight;

        public bool SetEdgeWeight(
            TKey sourceKey,
            TKey targetKey,
            TWeight weight)
        {
            if (!AdjacencyList.ContainsKey(sourceKey))
            {
                return false;
            }

            var weightedEdge = AdjacencyList[sourceKey]
                .FirstOrDefault(
                    node =>
                    targetKey.Equals(node.Target));
            if (weightedEdge is null)
            {
                return false;
            }

            var newEdge = new WeightedEdge<TKey, TWeight>(
                weightedEdge.Source,
                weightedEdge.Target,
                weight) as TEdge;
            if (newEdge is null)
            {
                return false;
            }

            AdjacencyList[sourceKey].Remove(weightedEdge);
            AdjacencyList[sourceKey].AddLast(newEdge);
            return true;
        }
    }
}
