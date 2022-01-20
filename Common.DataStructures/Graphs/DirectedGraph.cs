namespace Common.DataStructures.Graphs
{
    using Common.DataStructures.Graphs.Base;
    using System;
    using System.Collections.Generic;

    public class DirectedGraph<TKey, TVertex> : Graph<TKey, TVertex>
        where TKey : IEquatable<TKey>
    {
        public DirectedGraph()
            : base()
        {
            AdjacencyList = new Dictionary<TKey, LinkedList<TKey>>();
        }

        protected IDictionary<TKey, LinkedList<TKey>> AdjacencyList
        {
            get;
        }

        public override bool AddEdge(
            TKey sourceKey, 
            TKey targetKey)
        {
            if (!Vertices.Contains(sourceKey)
                || !AdjacencyList.ContainsKey(sourceKey))
            {
                throw new KeyNotFoundException(
                    $"Vertex (key: {sourceKey}) was not found.",
                    new ArgumentOutOfRangeException());
            }
            
            if (!Vertices.Contains(targetKey)
                || !AdjacencyList.ContainsKey(targetKey))
            {
                throw new KeyNotFoundException(
                    $"Vertex (key: {targetKey}) was not found.",
                    new ArgumentOutOfRangeException());
            }

            AdjacencyList[sourceKey].AddLast(targetKey);
            return true;
        }

        public override bool AddVertex(
            TKey vertexKey)
        {
            if (Vertices.Contains(vertexKey))
            {
                return false;
            }

            if (AdjacencyList.ContainsKey(vertexKey))
            {
                return false;
            }

            AdjacencyList.Add(
                vertexKey,
                new LinkedList<TKey>());
            Vertices.Add(vertexKey);
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
                        vertex,
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

                var neighbors = AdjacencyList[currentVertex];
                foreach(
                    var neighbor
                    in neighbors)
                {
                    if (visitedVertices[neighbor])
                    {
                        continue;
                    }

                    visitedVertices[neighbor] = true;
                    queue.AddLast(neighbor);
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
                    vertex => vertex,
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

                var neighbors = AdjacencyList[currentVertex];
                foreach (
                    var neighbor
                    in neighbors)
                {
                    if (visitedVertices[neighbor])
                    {
                        continue;
                    }

                    visitedVertices[neighbor] = true;
                    queue.AddFirst(neighbor);
                    pathMap[neighbor] = currentVertex;
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

        public override ICollection<TKey> GetNeighbors(TKey sourceKey)
        {
            throw new NotImplementedException();
        }

        public override TVertex? GetVertexValue(TKey vertexKey)
        {
            throw new NotImplementedException();
        }

        public override bool IsAdjacent(TKey sourceKey, TKey targetKey)
        {
            throw new NotImplementedException();
        }

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
