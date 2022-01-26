namespace Common.DataStructures.Graphs.Nodes;

public delegate void NodeAction<in TKey>(
    TKey nodeKey);