namespace Common.DataStructures.Graphs.Nodes;

using Common.DataStructures.Graphs.Contracts;
using Common.ExtensionLibrary;
using System.Collections.Generic;

public class MatrixNodeSet<TKey, TValue>
    : NodeSet<TKey, TValue>,
        IMutableNodeSet<TKey, TValue>
    where TKey : IEquatable<TKey>
{
    public override IEnumerable<TKey> Nodes
        => NodeToValueMapping.Keys;

    protected IDictionary<TKey, TValue?> NodeToValueMapping
    {
        get;
    }

    public MatrixNodeSet()
        => NodeToValueMapping = new Dictionary<TKey, TValue?>();

    public bool AddNode(
        TKey nodeKey)
    {
        if (NodeToValueMapping.ContainsKey(nodeKey))
        {
            return false;
        }

        NodeToValueMapping.Add(
            nodeKey,
            default);

        return true;
    }

    public int AddNodes(
        IEnumerable<TKey> nodeKeys)
        => nodeKeys.Count(AddNode);

    public bool Clear()
    {
        NodeToValueMapping.Clear();

        return Nodes.IsNullOrEmpty();
    }

    public override bool ContainsNode(
        TKey nodeKey)
        => NodeToValueMapping.ContainsKey(nodeKey);

    public override TValue? GetNodeValue(
            TKey nodeKey)
        => NodeToValueMapping.ContainsKey(nodeKey)
            ? NodeToValueMapping[nodeKey]
            : default;

    public bool RemoveNode(
        TKey nodeKey)
        => NodeToValueMapping.Remove(nodeKey);

    public int RemoveNodes(
        IEnumerable<TKey> nodeKeys)
        => nodeKeys.Count(RemoveNode);

    public override bool SetNodeValue(
        TKey nodeKey,
        TValue? nodeValue)
    {
        if (NodeToValueMapping.ContainsKey(nodeKey))
        {
            NodeToValueMapping[nodeKey] = nodeValue;

            return true;
        }

        return false;
    }
}