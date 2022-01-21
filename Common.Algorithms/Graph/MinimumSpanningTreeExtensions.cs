﻿namespace Common.Algorithms.Graph
{
    using Common.DataStructures.Contracts;
    using Common.ExtensionLibrary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MinimumSpanningTreeExtensions
    {
        public static IDictionary<TKey, TWeight> GetPrimsMinimumSpanningTree<TKey, TNode, TWeight>(
            this IWeightedGraphStorage<TKey, TNode, TWeight> weightedGraph,
            TWeight minimumWeight,
            TWeight maximumWeight)
            where TKey : IEquatable<TKey>
            where TWeight : IComparable<TWeight>
        {
            var minimumSpanningTree = new Dictionary<TKey, TWeight>();
            var vertexCount = 0;
            var vertexCosts = new Dictionary<TKey, TWeight>();
            bool hasAddedSeedVertex = false;

            foreach(
                var vertex 
                in weightedGraph.GetNodes())
            {
                vertexCount++;
                if (hasAddedSeedVertex)
                {
                    vertexCosts.Add(
                       vertex,
                       maximumWeight);
                    continue;
                }

                vertexCosts.Add(
                    vertex,
                    minimumWeight);
                hasAddedSeedVertex = true;
            }

            while(minimumSpanningTree.Count < vertexCount)
            {
                var lowestCost = vertexCosts
                    .Where(
                        keyValuePair =>
                        !minimumSpanningTree.ContainsKey(keyValuePair.Key))
                    .OrderBy(
                        keyValuePair => 
                        keyValuePair.Value)
                    .ThenBy(
                        keyValuePair =>
                        keyValuePair.Key)
                    .First();

                minimumSpanningTree.Add(
                    lowestCost.Key,
                    vertexCosts[lowestCost.Key]);

                var neighbors = weightedGraph
                    .GetNeighbors(lowestCost.Key)
                    .Where(
                        neighbor => 
                        !minimumSpanningTree.ContainsKey(neighbor));
                foreach(
                    var neighbor
                    in neighbors)
                {
                    var modifiedWeight = weightedGraph.GetEdgeWeight(
                        lowestCost.Key,
                        neighbor);
                    if (modifiedWeight.IsLessThan(vertexCosts[neighbor]))
                    {
                        vertexCosts[neighbor] = modifiedWeight;
                    }
                }
            }

            return minimumSpanningTree;
        }
    }
}
