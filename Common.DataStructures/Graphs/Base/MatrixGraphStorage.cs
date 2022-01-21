namespace Common.DataStructures.Graphs.Base
{
    using Common.DataStructures.Contracts;
    using System;
    using System.Collections.Generic;

    public class MatrixGraphStorage<TKey, TNode, TWeight> : IWeightedGraphStorage<TKey, TNode, TWeight>
        where TKey : IEquatable<TKey>
        where TWeight : IComparable<TWeight>
    {
        public MatrixGraphStorage(
            bool isDirected = false,
            TWeight ? offWeight = default)
        {
            if (offWeight is null)
            {
                throw new ArgumentNullException(nameof(offWeight));
            }

            EdgeMatrix = new bool[0, 0];
            EdgeWeights = new TWeight[0, 0];
            IsDirected = isDirected;
            NodeIndex = new Dictionary<TKey, int>();
            NodeIndexTracker = 0;
            NodeMapping = new Dictionary<TKey, TNode?>();
            OffWeight = offWeight;
        }

        protected bool[,] EdgeMatrix
        {
            get;
            set;
        }

        protected TWeight[,] EdgeWeights
        {
            get;
            set;
        }

        public bool IsDirected
        {
            get;
        }

        protected IDictionary<TKey, int> NodeIndex
        {
            get;
        }

        public int NodeIndexTracker
        {
            get;
            protected set;
        }

        protected IDictionary<TKey, TNode?> NodeMapping
        {
            get;
        }

        public virtual TWeight OffWeight
        {
            get;
            protected set;
        }

        public bool AddEdge(
            TKey sourceNodeKey,
            TKey neighborNodeKey)
            => SetEdgeState(
                sourceNodeKey,
                neighborNodeKey,
                true,
                OffWeight);

        public bool AddNode(
            TKey nodeKey)
        {
            if (NodeMapping.ContainsKey(nodeKey))
            {
                return false;
            }

            NodeMapping.Add(
                nodeKey,
                default);
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
                || !NodeIndex.ContainsKey(neighborNodeKey))
            {
                return false;
            }

            int sourceNodeIndex = NodeIndex[sourceNodeKey];
            int targetNodeIndex = NodeIndex[neighborNodeKey];
            int edgeWeightLength = GetEdgeWeightsLength();
            if (sourceNodeIndex >= edgeWeightLength
                || targetNodeIndex >= edgeWeightLength)
            {
                return false;
            }

            return EdgeMatrix[sourceNodeIndex, targetNodeIndex];
        }

        protected void CopyEdgesToNewArray(
            int newSize)
        {
            int edgeWeightsLength = GetEdgeWeightsLength();
            var newEdgeMatrix = new bool[newSize, newSize];
            var newEdgeWeights = new TWeight[newSize, newSize];

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

                    newEdgeMatrix[sourceIndex, targetIndex] = EdgeMatrix[sourceIndex, targetIndex];
                    newEdgeWeights[sourceIndex, targetIndex] = EdgeWeights[sourceIndex, targetIndex];
                }
            }

            EdgeMatrix = newEdgeMatrix;
            EdgeWeights = newEdgeWeights;
        }

        protected void CopyEdgesToNewArrayWithoutVertex(
            TKey vertexKeyToExclude)
        {
            if (!NodeIndex.ContainsKey(vertexKeyToExclude))
            {
                return;
            }

            int edgeWeightsLength = GetEdgeWeightsLength();
            var indexToExclude = NodeIndex[vertexKeyToExclude];

            var newMatrixSize = EdgeWeights.Length - 1;
            var newEdgeMatrix = new bool[newMatrixSize, newMatrixSize];
            var newEdgeWeights = new TWeight[newMatrixSize, newMatrixSize];

            for (int sourceIndex = 0;
                sourceIndex < edgeWeightsLength;
                sourceIndex++)
            {
                for (int targetIndex = 0;
                    targetIndex < edgeWeightsLength;
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

                    newEdgeWeights[newSourceIndex, newTargetIndex] = EdgeWeights[sourceIndex, targetIndex];
                    newEdgeMatrix[newSourceIndex, newTargetIndex] = EdgeMatrix[newSourceIndex, newTargetIndex];
                }
            }

            EdgeMatrix = newEdgeMatrix;
            EdgeWeights = newEdgeWeights;
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
                    var isEdge = EdgeMatrix[sourceIndex, targetIndex];
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

        public TWeight GetEdgeWeight(
            TKey sourceKey,
            TKey targetKey)
        {
            if (!NodeIndex.ContainsKey(sourceKey)
                || !NodeIndex.ContainsKey(targetKey))
            {
                return OffWeight;
            }

            int sourceNodeIndex = NodeIndex[sourceKey];
            int targetNodeIndex = NodeIndex[targetKey];
            if (sourceNodeIndex >= EdgeWeights.Length
                || targetNodeIndex >= EdgeWeights.Length)
            {
                return OffWeight;
            }

            return EdgeWeights[sourceNodeIndex, targetNodeIndex];
        }

        protected int GetEdgeWeightsLength()
            => (int)Math.Sqrt(EdgeWeights.Length);

        public ICollection<TKey> GetNeighbors(
            TKey sourceNodeKey)
        {
            ICollection<TKey> neighbors = new List<TKey>();
            if (!NodeIndex.ContainsKey(sourceNodeKey))
            {
                return neighbors;
            }

            var edgeWeightsLength = GetEdgeWeightsLength();
            int sourceNodeIndex = NodeIndex[sourceNodeKey];
            if (sourceNodeIndex >= edgeWeightsLength)
            {
                return neighbors;
            }

            foreach (
                TKey neighborKey
                in NodeIndex.Keys)
            {
                int neighborNodeIndex = NodeIndex[neighborKey];
                if (neighborNodeIndex >= edgeWeightsLength)
                {
                    continue;
                }

                var areAdjacent = EdgeMatrix[sourceNodeIndex, neighborNodeIndex]
                    || (!IsDirected
                        && EdgeMatrix[neighborNodeIndex, sourceNodeIndex]);
                if (areAdjacent)
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
                neighborNodeKey,
                false,
                OffWeight);

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
            bool areAdjacent,
            TWeight weightState)
        {
            if (!NodeIndex.ContainsKey(sourceKey)
                || !NodeIndex.ContainsKey(targetKey))
            {
                return false;
            }

            int edgeWeightsLength = GetEdgeWeightsLength();
            int neighborIndex = NodeIndex[targetKey];
            int sourceIndex = NodeIndex[sourceKey];
            if (sourceIndex >= edgeWeightsLength
                || neighborIndex >= edgeWeightsLength)
            {
                return false;
            }

            var isEdge = EdgeMatrix[sourceIndex, neighborIndex];
            if (!(areAdjacent || isEdge))
            {
                return false;
            }

            EdgeMatrix[sourceIndex, neighborIndex] = areAdjacent;
            if (weightState is null)
            {
                return true;
            }

            EdgeWeights[sourceIndex, neighborIndex] = weightState;
            if (IsDirected
                || sourceIndex == neighborIndex)
            {
                return true;
            }

            EdgeMatrix[neighborIndex, sourceIndex] = areAdjacent;
            EdgeWeights[neighborIndex, sourceIndex] = weightState;
            return true;
        }

        public bool SetEdgeWeight(
            TKey sourceKey, 
            TKey targetKey,
            TWeight weight)
            => SetEdgeState(
                sourceKey,
                targetKey,
                true,
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
    }
}
