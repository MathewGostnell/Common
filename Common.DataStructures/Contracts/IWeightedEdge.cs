namespace Common.DataStructures.Contracts
{
    public interface IWeightedEdge<TKey, TWeight> : IEdge<TKey>
        where TWeight : IComparable<TWeight>
    {
        public TWeight? Weight
        {
            get;
        }
    }
}
