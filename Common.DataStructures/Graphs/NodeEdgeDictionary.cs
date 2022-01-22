namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Graphs.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public sealed class NodeEdgeDictionary<TKey, TEdge>
        : Dictionary<TKey, IEdgeList<TKey, TEdge>>,
            ICloneable,
            INodeEdgeDictionary<TKey, TEdge>,
            ISerializable
        where TEdge : IEdge<TKey>
        where TKey : IEquatable<TKey>
    {
        public NodeEdgeDictionary()
        {
        }

        public NodeEdgeDictionary(
            int capacity)
            : base(capacity)
        {
        }

        public NodeEdgeDictionary(
            SerializationInfo serializationEntries,
            StreamingContext context)
            : base(
                serializationEntries,
                context)
        {
        }

        public NodeEdgeDictionary<TKey, TEdge> Clone()
        {
            var clonedDictionary = new NodeEdgeDictionary<TKey, TEdge>(Count);
            foreach (
                var keyValuePair
                in this)
            {
                clonedDictionary.Add(
                    keyValuePair.Key,
                    keyValuePair.Value.Clone());
            }

            return clonedDictionary;
        }

        object ICloneable.Clone()
            => Clone();

        INodeEdgeDictionary<TKey, TEdge> INodeEdgeDictionary<TKey, TEdge>.Clone()
            => Clone();
    }
}
