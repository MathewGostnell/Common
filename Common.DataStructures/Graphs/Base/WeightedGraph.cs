namespace Common.DataStructures.Graphs.Base
{
    using Common.DataStructures.Contracts;
    using System;
    using System.Collections.Generic;

    public class WeightedGraph<TKey, TNode, TWeight> : IWeightedGraphStorage<TKey, TNode, TWeight>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
    {
        public WeightedGraph(
            IWeightedGraphStorage<TKey, TNode, TWeight> weightedGraphStorage)
        {
            Storage = weightedGraphStorage;
        }

        protected IWeightedGraphStorage<TKey, TNode, TWeight> Storage
        {
            get;
        }

        public virtual bool AddEdge(
            TKey sourceNodeKey,
            TKey neighborNodeKey)
            => Storage.AddEdge(
                sourceNodeKey,
                neighborNodeKey);

        public virtual bool AddNode(
            TKey nodeKey)
            => Storage.AddNode(nodeKey);

        public virtual bool AreAdjacent(
            TKey sourceNodeKey,
            TKey neighborNodeKey)
            => Storage.AreAdjacent(
                sourceNodeKey,
                neighborNodeKey);

        public ICollection<IWeightedEdge<TKey, TWeight>> GetEdges()
            => Storage.GetEdges();

        public TWeight GetEdgeWeight(
            TKey sourceKey,
            TKey targetKey)
            => Storage.GetEdgeWeight(
                sourceKey,
                targetKey);

        public ICollection<TKey> GetNeighbors(
            TKey sourceNodeKey)
            => Storage.GetNeighbors(sourceNodeKey);

        public ICollection<TKey> GetNodes()
            => Storage.GetNodes();

        public TNode? GetNodeValue(
            TKey nodeKey)
            => Storage.GetNodeValue(nodeKey);

        public bool RemoveEdge(
            TKey sourceNodeKey,
            TKey neighborNodeKey)
            => Storage.RemoveEdge(
                sourceNodeKey,
                neighborNodeKey);

        public bool RemoveNode(
            TKey nodeKey)
            => Storage.RemoveNode(nodeKey);

        public bool SetEdgeWeight(
            TKey sourceKey,
            TKey targetKey,
            TWeight weight)
            => Storage.SetEdgeWeight(
                sourceKey,
                targetKey,
                weight);

        public bool SetNodeValue(
            TKey nodeKey,
            TNode? nodeValue)
            => Storage.SetNodeValue(
                nodeKey,
                nodeValue);
    }
}
