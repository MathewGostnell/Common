namespace Common.DataStructures.Graphs.Contracts;

internal interface INodeValueSet<TKey, TValue> : INodeSet<TKey>
{
    public TValue? GetNodeValue(
        TKey nodeKey);

    public bool SetNodeValue(
        TKey nodeKey,
        TValue? nodeValue);
}