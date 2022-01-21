namespace Common.DataStructures.Graphs.Base
{
    using Common.DataStructures.Contracts;
    using System.Collections.Generic;

    public class Graph<TKey, TNode> : IGraphStorage<Edge<TKey>, TKey, TNode>
        where TKey : IEquatable<TKey>
    {
        public Graph(
            IGraphStorage<Edge<TKey>, TKey, TNode> storage)
        {
            Storage = storage;
        }

        public IGraphStorage<Edge<TKey>, TKey, TNode> Storage
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

        public ICollection<Edge<TKey>> GetEdges()
            => Storage.GetEdges();

        public virtual ICollection<TKey> GetNeighbors(
            TKey sourceNodeKey)
            => Storage.GetNeighbors(sourceNodeKey);

        public ICollection<TKey> GetNodes()
            => Storage.GetNodes();

        public virtual TNode? GetNodeValue(
            TKey nodeKey)
            => Storage.GetNodeValue(nodeKey);

        public virtual bool RemoveEdge(
            TKey sourceNodeKey,
            TKey neighborNodeKey)
            => Storage.RemoveEdge(
                sourceNodeKey,
                neighborNodeKey);

        public virtual bool RemoveNode(
            TKey nodeKey)
            => Storage.RemoveNode(nodeKey);

        public bool SetNodeValue(
            TKey nodeKey,
            TNode? nodeValue)
            => Storage.SetNodeValue(
                nodeKey,
                nodeValue);
    }
}
