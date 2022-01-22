namespace Common.DataStructures.Graphs
{
    public delegate void NodeEventHandler<TKey>(
        object sender,
        NodeEventArgs<TKey> eventArgs);
}
