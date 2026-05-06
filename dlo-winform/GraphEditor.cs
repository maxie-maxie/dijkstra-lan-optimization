using System.Drawing;
using System.Linq;

namespace dlo_winform;

public static class GraphEditor
{
    private static readonly Random _rng = new();

    public static void AddEdgeIfMissing(GraphData data, NetworkNode startNode, NetworkNode endNode, int weight)
    {
        if (startNode == null || endNode == null || startNode == endNode) return;

        bool exists = data.edgeList.Any(edge =>
            (edge.StartNode == startNode && edge.EndNode == endNode) ||
            (edge.StartNode == endNode && edge.EndNode == startNode));

        if (!exists)
        {
            data.edgeList.Add(new NetworkEdge
            {
                StartNode = startNode,
                EndNode = endNode,
                Weight = weight,
                TransferSpeedBytesPerSecond = GenerateTransferSpeed()
            });
        }
    }

    public static ToggleEdgeOutcome ToggleEdge(GraphData data, NetworkNode nodeA, NetworkNode nodeB)
    {
        return ToggleEdge(data, nodeA, nodeB, GenerateTransferSpeed());
    }

    public static ToggleEdgeOutcome ToggleEdge(GraphData data, NetworkNode nodeA, NetworkNode nodeB, long speed)
    {
        if (nodeA == null || nodeB == null || nodeA == nodeB) return ToggleEdgeOutcome.NoAction;

        var edge = data.edgeList.FirstOrDefault(e =>
            (e.StartNode == nodeA && e.EndNode == nodeB) ||
            (e.StartNode == nodeB && e.EndNode == nodeA));

        if (edge != null)
        {
            data.edgeList.Remove(edge);
            return ToggleEdgeOutcome.Removed;
        }

        data.edgeList.Add(new NetworkEdge { StartNode = nodeA, EndNode = nodeB, Weight = 1, TransferSpeedBytesPerSecond = speed });
        return ToggleEdgeOutcome.Created;
    }

    public static NetworkEdge? TryGetEdgeNearPoint(GraphData data, PointF point, float tolerance)
    {
        foreach (var edge in data.edgeList)
        {
            float dist = GeometryHelpers.DistanceToSegment(point, edge.StartNode.Position, edge.EndNode.Position);
            if (dist < tolerance)
                return edge;
        }
        return null;
    }

    public static bool CanPlaceNode(GraphData data, PointF position, int radius)
    {
        foreach (var node in data.nodeList)
        {
            if (GeometryHelpers.CirclesOverlap(position, radius, node.Position, node.Radius))
                return false;
        }
        return true;
    }

    public static NetworkNode AddNode(GraphData data, PointF position)
    {
        int id = 1;
        var usedIds = new HashSet<int>(data.nodeList.Select(n => n.Id));
        while (usedIds.Contains(id)) id++;
        var node = new NetworkNode { Id = id, Position = position };
        data.nodeList.Add(node);
        return node;
    }

    public static (bool nodeRemoved, int removedEdgeCount, List<NetworkEdge> removedEdges) RemoveNode(GraphData data, PointF position)
    {
        var node = data.GetNode(Point.Round(position));
        if (node == null) return (false, 0, new List<NetworkEdge>());

        var removedEdges = data.edgeList
            .Where(e => e.StartNode == node || e.EndNode == node)
            .ToList();
        foreach (var edge in removedEdges)
            data.edgeList.Remove(edge);

        data.nodeList.Remove(node);
        return (true, removedEdges.Count, removedEdges);
    }

    public static long GenerateTransferSpeed()
    {
        long[] bases = { 12_500_000L, 37_500_000L, 125_000_000L, 1_250_000_000L };
        long baseSpeed = bases[_rng.Next(bases.Length)];
        double factor = 0.5 + _rng.NextDouble();
        return Math.Max(1_000_000L, (long)(baseSpeed * factor));
    }

    public static long PacketSizeBytes(int value, PacketUnit unit)
    {
        if (value <= 0) return 0;
        return unit switch
        {
            PacketUnit.Bytes => value,
            PacketUnit.KB => value * 1024L,
            PacketUnit.MB => value * 1024L * 1024,
            PacketUnit.GB => value * 1024L * 1024 * 1024,
            _ => value
        };
    }

    public static int ComputeEdgeWeightMs(long packetBytes, long speedBytesPerSec)
    {
        if (speedBytesPerSec <= 0) return int.MaxValue;
        long ms = packetBytes * 1000L / speedBytesPerSec;
        return ms > 0 ? (int)ms : 1;
    }

    public static void RecalculateEdgeWeights(GraphData data, long packetBytes)
    {
        foreach (var edge in data.edgeList)
        {
            edge.Weight = ComputeEdgeWeightMs(packetBytes, edge.TransferSpeedBytesPerSecond);
        }
    }

    public static string SpeedToMbpsString(long speedBytesPerSec)
    {
        double mbps = speedBytesPerSec / 1_000_000.0;
        return mbps.ToString("F1");
    }
}
