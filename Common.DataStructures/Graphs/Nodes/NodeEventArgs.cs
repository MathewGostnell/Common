namespace Common.DataStructures.Graphs.Nodes;

using System;

public class NodeEventArgs<TKey> : EventArgs
{
    public TKey Node
    {
        get;
    }

    public NodeEventArgs(
        TKey nodeKey)
    {
        if (nodeKey is null)
        {
            throw new ArgumentNullException(nameof(nodeKey));
        }

        Node = nodeKey;
    }
}