namespace Common.DataStructures.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IGraphStorage<TEdge, TKey, TNode>
        where TEdge : IEdge<TKey>
        where TKey : IEquatable<TKey>
    {
        public bool AddEdge(
            TKey sourceNodeKey,
            TKey neighborNodeKey);

        public bool AddNode(
            TKey nodeKey);

        public bool AreAdjacent(
            TKey sourceNodeKey,
            TKey neighborNodeKey);

        public ICollection<TEdge> GetEdges();

        public ICollection<TKey> GetNeighbors(
            TKey sourceNodeKey);

        public ICollection<TKey> GetNodes();

        public TNode? GetNodeValue(
            TKey nodeKey);

        public bool RemoveEdge(
            TKey sourceNodeKey,
            TKey neighborNodeKey);

        public bool RemoveNode(
            TKey nodeKey);

        public bool SetNodeValue(
            TKey nodeKey,
            TNode? nodeValue);
    }
}
