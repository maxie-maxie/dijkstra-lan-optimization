using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace dlo_winform;

public static class RandomGraphGenerator
{
    public static GraphData Generate(int nodeCount, Size canvasSize)
    {
        return GenerateTree(nodeCount, canvasSize, new Random());
    }

    public static GraphData GenerateTree(int nodeCount, Size canvasSize, Random random)
    {
        var data = new GraphData();
        int radius = 10;
        int margin = 40;
        int spacing = radius * 2 + 8;
        int maxAttemptsPerNode = 100;

        int xMin = margin;
        int xMax = canvasSize.Width - margin;
        int yMin = margin;
        int yMax = canvasSize.Height - margin;

        if (xMax <= xMin || yMax <= yMin)
        {
            return FallbackGrid(data, nodeCount, canvasSize, radius, spacing, margin);
        }

        int id = 1;
        float firstX = random.Next(xMin, xMax);
        float firstY = random.Next(yMin, yMax);
        data.nodeList.Add(new NetworkNode { Id = id++, Position = new PointF(firstX, firstY), Radius = radius });

        for (int n = 1; n < nodeCount; n++)
        {
            PointF? placedPosition = null;
            int bestTargetIndex = -1;

            for (int attempt = 0; attempt < maxAttemptsPerNode; attempt++)
            {
                float px = random.Next(xMin, xMax);
                float py = random.Next(yMin, yMax);
                var candidate = new PointF(px, py);

                if (!IsValidNodePosition(candidate, data.nodeList, spacing))
                    continue;

                int targetIndex = FindClosestNodeIndex(candidate, data.nodeList);
                if (targetIndex < 0)
                    continue;

                var proposedEdge = new NetworkEdge
                {
                    StartNode = data.nodeList[targetIndex],
                    EndNode = new NetworkNode { Position = candidate }
                };

                if (EdgeCrossesExisting(proposedEdge, data.edgeList))
                    continue;

                if (EdgePassesThroughOtherNode(proposedEdge, data.nodeList, targetIndex))
                    continue;

                placedPosition = candidate;
                bestTargetIndex = targetIndex;
                break;
            }

            if (placedPosition == null)
            {
                int cols = (int)System.Math.Ceiling(System.Math.Sqrt(nodeCount));
                int gridIdx = data.nodeList.Count;
                int col = gridIdx % cols;
                int row = gridIdx / cols;
                float gx = xMin + col * spacing;
                float gy = yMin + row * spacing;
                gx = System.Math.Min(gx, xMax);
                gy = System.Math.Min(gy, yMax);
                placedPosition = new PointF(gx, gy);

                int targetIndex = FindClosestNodeIndex(placedPosition.Value, data.nodeList);
                if (targetIndex < 0)
                    targetIndex = 0;

                var pe = new NetworkEdge
                {
                    StartNode = data.nodeList[targetIndex],
                    EndNode = new NetworkNode { Position = placedPosition.Value }
                };

                if (EdgeCrossesExisting(pe, data.edgeList) ||
                    EdgePassesThroughOtherNode(pe, data.nodeList, targetIndex))
                {
                    targetIndex = 0;
                }

                bestTargetIndex = targetIndex;
            }

            var newNode = new NetworkNode { Id = id++, Position = placedPosition.Value, Radius = radius };
            data.nodeList.Add(newNode);

            int weight = random.Next(1, 21);
            data.edgeList.Add(new NetworkEdge
            {
                StartNode = data.nodeList[bestTargetIndex],
                EndNode = newNode,
                Weight = weight
            });
        }

        return data;
    }

    private static GraphData FallbackGrid(GraphData data, int nodeCount, Size canvasSize, int radius, int spacing, int margin)
    {
        int xMin = margin;
        int yMin = margin;
        int xMax = canvasSize.Width - margin;

        int cols = (int)System.Math.Ceiling(System.Math.Sqrt(nodeCount));

        for (int i = 0; i < nodeCount; i++)
        {
            int col = i % cols;
            int row = i / cols;
            float gx = xMin + col * spacing;
            float gy = yMin + row * spacing;
            gx = System.Math.Min(gx, xMax);
            var node = new NetworkNode { Id = i + 1, Position = new PointF(gx, gy), Radius = radius };
            data.nodeList.Add(node);

            if (i > 0)
            {
                int parent = (i - 1) / 2;
                data.edgeList.Add(new NetworkEdge
                {
                    StartNode = data.nodeList[parent],
                    EndNode = node,
                    Weight = 10
                });
            }
        }

        return data;
    }

    private static bool IsValidNodePosition(PointF candidate, List<NetworkNode> existingNodes, int spacing)
    {
        foreach (var node in existingNodes)
        {
            if (GeometryHelpers.Distance(candidate, node.Position) < spacing)
                return false;
        }
        return true;
    }

    private static int FindClosestNodeIndex(PointF position, List<NetworkNode> nodes)
    {
        float bestDist = float.MaxValue;
        int bestIndex = -1;
        for (int i = 0; i < nodes.Count; i++)
        {
            float d = GeometryHelpers.Distance(position, nodes[i].Position);
            if (d < bestDist)
            {
                bestDist = d;
                bestIndex = i;
            }
        }
        return bestIndex;
    }

    private static bool EdgeCrossesExisting(NetworkEdge proposedEdge, List<NetworkEdge> existingEdges)
    {
        foreach (var existing in existingEdges)
        {
            if (GeometryHelpers.SegmentsIntersect(
                    proposedEdge.StartNode.Position, proposedEdge.EndNode.Position,
                    existing.StartNode.Position, existing.EndNode.Position))
                return true;
        }
        return false;
    }

    private static bool EdgePassesThroughOtherNode(NetworkEdge proposedEdge, List<NetworkNode> allNodes, int targetIndex)
    {
        for (int i = 0; i < allNodes.Count; i++)
        {
            if (i == targetIndex)
                continue;
            if (GeometryHelpers.EdgePassesThroughNode(proposedEdge, allNodes[i]))
                return true;
        }
        return false;
    }
}
