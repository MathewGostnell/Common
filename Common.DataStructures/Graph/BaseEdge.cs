namespace Common.DataStructures.Graph
{
    using Common.DataStructures.Graph.Contracts;

    public class BaseEdge<TVertex> : IEdge<TVertex>
    {
        public BaseEdge(
            TVertex source,
            TVertex target)
        {
            Source = source;
            Target = target;
        }

        public TVertex Source
        {
            get;
        }

        public TVertex Target
        {
            get;
        }
    }
}
