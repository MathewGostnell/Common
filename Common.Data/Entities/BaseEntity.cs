namespace Common.Data.Entities;

using Common.Data.Contracts;

public abstract class BaseEntity<TKey> : IIdEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual TKey? Id
    {
        get;
        set;
    }
}