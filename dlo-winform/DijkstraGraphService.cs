using Graph;
using System.Collections.Generic;
using System.Linq;

namespace dlo_winform;

public sealed class DijkstraRouteResult
{
    public int StartNodeId { get; init; }
    public int DestinationNodeId { get; init; }
    public bool Reachable { get; init; }
    public long TotalTime { get; init; }
    public IReadOnlyList<int> PathNodeIds { get; init; } = new List<int>();
    public IReadOnlyList<NetworkEdge> PathEdges { get; init; } = new List<NetworkEdge>();
}

public static class DijkstraGraphService
{
    public static DijkstraRouteResult FindRoute(GraphData data, int startNodeId, int endNodeId)
    {
        // Validate inputs
        if (data.nodeList.Count == 0 || data.edgeList.Count == 0)
        {
            return new DijkstraRouteResult
            {
                StartNodeId = startNodeId,
                DestinationNodeId = endNodeId
            };
        }

        var startNode = data.GetNodeById(startNodeId);
        var endNode = data.GetNodeById(endNodeId);
        if (startNode == null || endNode == null)
        {
            return new DijkstraRouteResult
            {
                StartNodeId = startNodeId,
                DestinationNodeId = endNodeId
            };
        }

        // Find max node ID to size the EdgeGraph
        int maxId = 0;
        foreach (var node in data.nodeList)
            if (node.Id > maxId) maxId = node.Id;

        var graph = new EdgeGraph(maxId);

        // Add edges
        foreach (var edge in data.edgeList)
        {
            if (edge.Weight < 0) continue; // Skip negative weights
            graph.AddEdge(edge.StartNode.Id, edge.EndNode.Id, edge.Weight);
        }

        // Run Dijkstra
        var distances = graph.Dijkstra(startNodeId);

        // Check if reachable
        if (distances[endNodeId] >= graph.Infinity)
        {
            return new DijkstraRouteResult
            {
                StartNodeId = startNodeId,
                DestinationNodeId = endNodeId,
                Reachable = false
            };
        }

        // Get path
        var pathIds = graph.tracePath(startNodeId, endNodeId);

        // Build path edges
        var pathEdges = new List<NetworkEdge>();
        for (int i = 0; i < pathIds.Count - 1; i++)
        {
            int fromId = pathIds[i];
            int toId = pathIds[i + 1];
            foreach (var edge in data.edgeList)
            {
                if ((edge.StartNode.Id == fromId && edge.EndNode.Id == toId) ||
                    (edge.StartNode.Id == toId && edge.EndNode.Id == fromId))
                {
                    pathEdges.Add(edge);
                    break;
                }
            }
        }

        return new DijkstraRouteResult
        {
            StartNodeId = startNodeId,
            DestinationNodeId = endNodeId,
            Reachable = true,
            TotalTime = distances[endNodeId],
            PathNodeIds = pathIds,
            PathEdges = pathEdges
        };
    }
}
