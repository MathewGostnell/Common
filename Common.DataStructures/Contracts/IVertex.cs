namespace Common.DataStructures.Contracts
{
    public interface IVertex<TValue>
    {
        public TValue? Value
        {
            get;
        }

        public void SetValue(
            TValue? newValue);
    }
}
