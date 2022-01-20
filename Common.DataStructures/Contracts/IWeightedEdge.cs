namespace Common.DataStructures.Contracts
{
    public interface IWeightedEdge<TVertex, TWeight> : IEdge<TVertex>
    {
        public TWeight Weight
        {
            get;
        }
    }
}
