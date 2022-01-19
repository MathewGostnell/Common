namespace Common.DataStructures.Graph
{
    using Common.DataStructures.Graph.Contracts;
    using System;

    public class BaseVertex<TKey, TValue> : IVertex<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        public BaseVertex(
            TKey key,
            TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key
        {
            get;
        }

        public TValue Value
        {
            get;
        }
    }
}
