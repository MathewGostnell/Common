namespace Common.DataStructures.Graphs.Handlers;

using Common.DataStructures.Graphs.Events;

public delegate void NodeEventHandler<TKey>(
    object sender,
    NodeEventArgs<TKey> eventArgs);