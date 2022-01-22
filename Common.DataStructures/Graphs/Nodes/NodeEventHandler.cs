namespace Common.DataStructures.Graphs.Nodes;

public delegate void NodeEventHandler<TKey>(
    object sender,
    NodeEventArgs<TKey> nodeEventArgs);