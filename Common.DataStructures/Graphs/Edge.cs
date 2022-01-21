namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Contracts;
    using System;

    public class Edge<TKey> : IEdge<TKey>
        where TKey : IEquatable<TKey>
    {
        public Edge(
            TKey source,
            TKey target)
        {
            Source = source;
            Target = target;
        }

        public TKey Source
        {
            get;
        }

        public TKey Target
        {
            get;
        }
    }
}
