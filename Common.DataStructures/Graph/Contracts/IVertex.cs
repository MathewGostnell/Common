namespace Common.DataStructures.Graph.Contracts
{
    using System;

    public interface IVertex<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
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
