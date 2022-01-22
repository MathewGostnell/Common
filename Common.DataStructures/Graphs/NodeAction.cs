namespace Common.DataStructures.Graphs
{
    public delegate void NodeAction<in TKey>(
        TKey nodeKey);
}
