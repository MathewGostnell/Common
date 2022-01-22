namespace Common.DataStructures.Graphs.Contracts
{
    public interface IEdge<TKey>
    {
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
