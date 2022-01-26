namespace Common.DataStructures.Graphs.Contracts;

public interface IEdge<TKey>
{
    public TKey SourceKey
    {
        get;
    }

    public TKey TargetKey
    {
        get;
    }
}