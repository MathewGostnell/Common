namespace Common.DataStructures.Tests.GraphsTests.Edges;

using Common.DataStructures.Graphs.Contracts;

public class TestEdge<TKey> : IEdge<TKey>
{
    public TestEdge(
        TKey source,
        TKey target)
    {
        SourceKey = source;
        TargetKey = target;
    }

    public TKey SourceKey
    {
        get;
        protected set;
    }

    public TKey TargetKey
    {
        get;
        protected set;
    }
}