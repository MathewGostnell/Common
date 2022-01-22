namespace Common.DataStructures.Graphs.Actions;

public delegate void NodeAction<in TKey>(
    TKey nodeKey);