namespace Common.DataStructures.Contracts
{
    public interface IWeightedEdge<TKey, TWeight> : IEdge<TKey>
    {
        public TWeight Weight
        {
            get;
        }
    }
}
