namespace Common.DataStructures.Graphs.Base
{
    using Common.DataStructures.Contracts;
    using System;
    using System.Collections.Generic;

    public class MatrixGraphStorage<TKey, TNode, TWeight> : IWeightedGraphStorage<IWeightedEdge<TKey, TWeight>, TKey, TNode, TWeight>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
    {
        public MatrixGraphStorage(
            bool isDirected = false,
            TWeight? offWeight = default)
        {
            EdgeWeights = new TWeight?[0, 0];
            IsDirected = isDirected;
            NodeIndex = new Dictionary<TKey, int>();
            NodeIndexTracker = 0;
            NodeMapping = new Dictionary<TKey, TNode?>();
            OffWeight = offWeight;
        }

        protected TWeight?[,] EdgeWeights
        {
            get;
            set;
        }

        public bool IsDirected
        {
            get;
        }

        public IDictionary<TKey, int> NodeIndex
        {
            get;
        }

        public int NodeIndexTracker
        {
            get;
            protected set;
        }

        public IDictionary<TKey, TNode?> NodeMapping
        {
            get;
        }

        public virtual TWeight? OffWeight
        {
            get;
        }

        public bool AddEdge(
            TKey sourceNodeKey,
            TKey neighborNodeKey)
            => SetEdgeState(
                sourceNodeKey,
                neighborNodeKey);

        public bool AddNode(
            TKey nodeKey)
        {
            if (NodeMapping.ContainsKey(nodeKey))
            {
                return false;
            }

            NodeMapping.Add(
                nodeKey,
                default(TNode));
            NodeIndex.Add(
                nodeKey,
                NodeIndexTracker++);
            CopyEdgesToNewArray(NodeIndexTracker);
            return true;
        }

        public bool AreAdjacent(
            TKey sourceNodeKey, 
            TKey neighborNodeKey)
        {
            if (!NodeIndex.ContainsKey(sourceNodeKey)
                || NodeIndex.ContainsKey(neighborNodeKey))
            {
                return false;
            }

            int sourceNodeIndex = NodeIndex[sourceNodeKey];
            int targetNodeIndex = NodeIndex[neighborNodeKey];
            if (sourceNodeIndex >= EdgeWeights.Length
                || targetNodeIndex >= EdgeWeights.Length)
            {
                return false;
            }

            return EdgeWeights[sourceNodeIndex, targetNodeIndex] != null;
        }

        protected void CopyEdgesToNewArray(
            int newSize)
        {
            var newEdgeMatrix = new TWeight?[newSize, newSize];
            int edgeWeightsLength = (int)Math.Sqrt(EdgeWeights.Length);
            for (int sourceIndex = 0;
                sourceIndex < edgeWeightsLength;
                sourceIndex++)
            {
                for (int targetIndex = 0;
                    targetIndex < edgeWeightsLength;
                    targetIndex++)
                {
                    if (sourceIndex >= newSize
                        || targetIndex >= newSize)
                    {
                        continue;
                    }

                    // TODO resolve index out of bounds error.
                    newEdgeMatrix[sourceIndex, targetIndex] = EdgeWeights[sourceIndex, targetIndex];
                }
            }
            EdgeWeights = newEdgeMatrix;
            if (EdgeWeights is null)
            {
                throw new ApplicationException();
            }

            EdgeWeights = newEdgeMatrix;
            if (EdgeWeights.Length != newSize * newSize)
            {
                throw new ApplicationException();
            }
        }

        protected void CopyEdgesToNewArrayWithoutVertex(
            TKey vertexKeyToExclude)
        {
            if (!NodeIndex.ContainsKey(vertexKeyToExclude))
            {
                return;
            }

            var indexToExclude = NodeIndex[vertexKeyToExclude];
            var newMatrixSize = EdgeWeights.Length - 1;
            var newEdgeMatrix = new TWeight?[newMatrixSize, newMatrixSize];

            for (int sourceIndex = 0;
                sourceIndex < EdgeWeights.Length;
                sourceIndex++)
            {
                for (int targetIndex = 0;
                    targetIndex < EdgeWeights.Length;
                    targetIndex++)
                {
                    if (sourceIndex >= newMatrixSize
                        || targetIndex >= newMatrixSize
                        || sourceIndex == indexToExclude
                        || targetIndex == indexToExclude)
                    {
                        continue;
                    }

                    int newSourceIndex = sourceIndex > indexToExclude
                        ? sourceIndex - 1
                        : sourceIndex;
                    int newTargetIndex = targetIndex > indexToExclude
                        ? targetIndex - 1
                        : targetIndex;

                    newEdgeMatrix[newSourceIndex, newTargetIndex] = EdgeWeights[sourceIndex, targetIndex];
                }
            }

            EdgeWeights = newEdgeMatrix;
        }

        public ICollection<IWeightedEdge<TKey, TWeight>> GetEdges()
        {
            var edges = new HashSet<IWeightedEdge<TKey, TWeight>>();
            foreach(
                var sourceKey
                in NodeMapping.Keys)
            {
                foreach(
                    var targetKey
                    in NodeMapping.Keys)
                {
                    int sourceIndex = NodeIndex[sourceKey];
                    int targetIndex = NodeIndex[targetKey];
                    var edgeWeight = EdgeWeights[sourceIndex, targetIndex];
                    var isEdge = edgeWeight != null 
                        && edgeWeight.CompareTo(OffWeight) != 0;
                    if (isEdge)
                    {
                        var newEdge = new WeightedEdge<TKey, TWeight>(
                            sourceKey,
                            targetKey,
                            EdgeWeights[sourceIndex, targetIndex]);
                        if (newEdge is null)
                        {
                            continue;
                        }

                        edges.Add(newEdge);
                    }
                }
            }

            return edges;
        }

        public ICollection<TKey> GetNeighbors(
            TKey sourceNodeKey)
        {
            ICollection<TKey> neighbors = new List<TKey>();
            if (!NodeIndex.ContainsKey(sourceNodeKey))
            {
                return neighbors;
            }

            int sourceNodeIndex = NodeIndex[sourceNodeKey];
            if (sourceNodeIndex >= EdgeWeights.Length)
            {
                return neighbors;
            }

            foreach (
                TKey neighborKey
                in NodeIndex.Keys)
            {
                int neighborNodeIndex = NodeIndex[neighborKey];
                if (neighborNodeIndex >= EdgeWeights.Length)
                {
                    continue;
                }

                var edgeWeight = EdgeWeights[sourceNodeIndex, neighborNodeIndex];
                if (edgeWeight is not null
                    && edgeWeight.CompareTo(OffWeight) != 0)
                {
                    neighbors.Add(neighborKey);
                }
            }

            return neighbors;
        }

        public ICollection<TKey> GetNodes()
            => NodeMapping.Keys;

        public TNode? GetNodeValue(
            TKey nodeKey)
            => NodeMapping.ContainsKey(nodeKey)
                ? NodeMapping[nodeKey]
                : default;

        public bool RemoveEdge(
            TKey sourceNodeKey, 
            TKey neighborNodeKey)
            => SetEdgeState(
                sourceNodeKey,
                neighborNodeKey);

        public bool RemoveNode(
            TKey nodeKey)
        {
            if (!NodeMapping.ContainsKey(nodeKey))
            {
                return false;
            }
            NodeIndex.Remove(nodeKey);
            NodeMapping.Remove(nodeKey);
            CopyEdgesToNewArrayWithoutVertex(nodeKey);
            return true;
        }

        protected bool SetEdgeState(
            TKey sourceKey,
            TKey targetKey,
            TWeight? weightState = default)
        {
            if (!NodeIndex.ContainsKey(sourceKey)
                || !NodeIndex.ContainsKey(targetKey))
            {
                return false;
            }

            int sourceIndex = NodeIndex[sourceKey];
            int neighborIndex = NodeIndex[targetKey];
            if (sourceIndex >= EdgeWeights.Length
                || neighborIndex >= EdgeWeights.Length)
            {
                return false;
            }

            EdgeWeights[sourceIndex, neighborIndex] = weightState;
            if (IsDirected)
            {
                return true;
            }

            EdgeWeights[neighborIndex, sourceIndex] = weightState;
            return true;
        }

        public bool SetEdgeWeight(
            TKey sourceKey, 
            TKey targetKey,
            TWeight? weight)
            => SetEdgeState(
                sourceKey,
                targetKey,
                weight);

        public bool SetNodeValue(
            TKey nodeKey, 
            TNode? nodeValue)
        {
            if (!NodeMapping.ContainsKey(nodeKey))
            {
                return false;
            }

            NodeMapping[nodeKey] = nodeValue;
            return true;
        }

        public TWeight? GetEdgeWeight(
            TKey sourceKey, 
            TKey targetKey)
        {
            if (!NodeIndex.ContainsKey(sourceKey)
                || !NodeIndex.ContainsKey(targetKey))
            {
                return default;
            }

            int sourceNodeIndex = NodeIndex[sourceKey];
            int targetNodeIndex = NodeIndex[targetKey];
            if (sourceNodeIndex >= EdgeWeights.Length
                || targetNodeIndex >= EdgeWeights.Length)
            {
                return default;
            }

            return EdgeWeights[sourceNodeIndex, targetNodeIndex];
        }
    }
}
