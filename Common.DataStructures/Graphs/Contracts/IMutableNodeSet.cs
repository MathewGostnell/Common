namespace Common.DataStructures.Graphs.Contracts;

using Common.DataStructures.Graphs.Actions;
using Common.DataStructures.Graphs.Predicates;
using System.Collections.Generic;

public interface IMutableNodeSet<TKey> : INodeSet<TKey>
{
    bool AddNode(
        TKey nodeKey);

    int AddNodes(
        IEnumerable<TKey> nodeKeys);

    public event NodeAction<TKey> NodeAdded;

    public event NodeAction<TKey> NodeRemoved;

    bool RemoveNode(
        TKey nodeKey);

    int RemoveNodes(
        IEnumerable<TKey> nodeKeys);

    int RemoveNodes(
        NodePredicate<TKey> predicate);
}