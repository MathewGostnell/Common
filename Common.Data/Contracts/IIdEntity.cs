namespace Common.Data.Contracts;

public interface IIdEntity<TKey>
{
    public TKey? Id
    {
        get;
    }
}