namespace Common.DataStructures.Graph.Contracts
{
    public interface IEdge<TVertex>
    {
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
