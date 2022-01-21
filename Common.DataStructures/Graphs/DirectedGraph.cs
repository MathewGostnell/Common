namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Contracts;
    using Common.DataStructures.Graphs.Base;
    using System;
    using System.Collections.Generic;

    public class DirectedGraph<TEdge, TKey, TVertex> : Graph<TEdge, TKey, TVertex>
        where TKey : IEquatable<TKey>
        where TEdge : class, IEdge<TKey>
    {
        public DirectedGraph()
            : base()
        {
            AdjacencyList = new Dictionary<TKey, LinkedList<TEdge>>();
        }

        protected IDictionary<TKey, LinkedList<TEdge>> AdjacencyList
        {
            get;
        }

        public override bool AddEdge(
            TKey sourceKey, 
            TKey targetKey)
        {
            if (!Vertices.ContainsKey(sourceKey)
                || !AdjacencyList.ContainsKey(sourceKey))
            {
                throw new KeyNotFoundException(
                    $"Vertex (key: {sourceKey}) was not found.",
                    new ArgumentOutOfRangeException());
            }
            
            if (!Vertices.ContainsKey(targetKey)
                || !AdjacencyList.ContainsKey(targetKey))
            {
                throw new KeyNotFoundException(
                    $"Vertex (key: {targetKey}) was not found.",
                    new ArgumentOutOfRangeException());
            }
            var edge = new WeightedEdge<TKey, int>(
                sourceKey,
                targetKey,
                1);
            var nodeEdge = new LinkedListNode<TEdge>(edge as TEdge);
            AdjacencyList[sourceKey].AddLast(nodeEdge);
            return true;
        }

        public override bool AddVertex(
            TKey vertexKey)
        {
            if (Vertices.ContainsKey(vertexKey))
            {
                return false;
            }

            if (AdjacencyList.ContainsKey(vertexKey))
            {
                return false;
            }

            AdjacencyList.Add(
                vertexKey,
                new LinkedList<TEdge>());
            Vertices.Add(
                vertexKey,
                default);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertexKey"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// <c>Runtime: O(V+E)</c>
        /// </remarks>
        public object? BreadthFirstSearch(
            TKey vertexKey)
        {
            var visitedVertices = Vertices
                .Select(
                    vertex =>
                    new KeyValuePair<TKey, bool>(
                        vertex.Key,
                        false))
                as IDictionary<TKey, bool>;
            if (visitedVertices is null)
            {
                throw new ArgumentNullException();
            }

            var queue = new LinkedList<TKey>();

            visitedVertices[vertexKey] = true;
            queue.AddLast(vertexKey);

            while(queue.Any())
            {
                var currentVertex = queue.First();
                queue.RemoveFirst();

                var relationships = AdjacencyList[currentVertex];
                foreach(
                    var relationship
                    in relationships)
                {
                    if (visitedVertices[relationship.Target])
                    {
                        continue;
                    }

                    visitedVertices[relationship.Target] = true;
                    queue.AddLast(relationship.Target);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertexKey"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Uses less memory compared to BFS
        /// </remarks>
        public IDictionary<TKey, TKey> DepthFirstSearch(
            TKey vertexKey)
        {
            var visitedVertices = Vertices
                .ToDictionary(
                    vertex => vertex.Key,
                    vertex => false);

            if (visitedVertices is null)
            {
                throw new ArgumentNullException();
            }

            var queue = new LinkedList<TKey>();
            var pathMap = new Dictionary<TKey, TKey>();

            visitedVertices[vertexKey] = true;
            queue.AddFirst(vertexKey);

            while (queue.Any())
            {
                var currentVertex = queue.First();
                queue.RemoveFirst();

                var relationships = AdjacencyList[currentVertex];
                foreach (
                    var relationship
                    in relationships)
                {
                    if (visitedVertices[relationship.Target])
                    {
                        continue;
                    }

                    visitedVertices[relationship.Target] = true;
                    queue.AddFirst(relationship.Target);
                    pathMap[relationship.Target] = currentVertex;
                }
            }

            return pathMap;
        }

        public IList<TKey> DepthFirstSearch(
            TKey sourceKey,
            TKey targetKey)
        {
            var pathMap = DepthFirstSearch(sourceKey);
            var currentTarget = targetKey;
            var path = new LinkedList<TKey>();

            while(pathMap.ContainsKey(currentTarget))
            {
                path.AddFirst(currentTarget);
                currentTarget = pathMap[currentTarget];
            }

            path.AddFirst(sourceKey);
            return path.ToList();
        }

        public override ICollection<TKey> GetNeighbors(
            TKey sourceKey)
            => AdjacencyList[sourceKey]
                .Select(
                    edge => 
                    edge.Target)
                .ToList();

        public override TVertex? GetVertexValue(
            TKey vertexKey)
        {
            throw new NotImplementedException();
        }

        public override bool IsAdjacent(
            TKey sourceKey, 
            TKey targetKey)
            => AdjacencyList[sourceKey]
                .Any(
                    node => 
                    node.Target
                        .Equals(targetKey));

        public override bool RemoveEdge(TKey source, TKey target)
        {
            throw new NotImplementedException();
        }

        public override bool RemoveVertex(TKey vertexKey)
        {
            throw new NotImplementedException();
        }

        public override bool SetVertexValue(TKey vertexKey, TVertex? vertexValue)
        {
            throw new NotImplementedException();
        }
    }
}
