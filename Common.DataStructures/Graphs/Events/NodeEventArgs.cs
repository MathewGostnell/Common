namespace Common.DataStructures.Graphs.Events;

public class NodeEventArgs<TKey> : EventArgs
{
    public NodeEventArgs(
        TKey nodeKey)
    {
        if (nodeKey is null)
        {
            throw new ArgumentNullException(nameof(nodeKey));
        }

        NodeKey = nodeKey;
    }

    public TKey NodeKey { get; }
}