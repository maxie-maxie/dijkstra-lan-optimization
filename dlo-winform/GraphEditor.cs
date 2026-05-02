using System.Drawing;
using System.Linq;

namespace dlo_winform;

public static class GraphEditor
{
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
                Weight = weight
            });
        }
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
        int id = data.nodeList.Count + 1;
        var node = new NetworkNode { Id = id, Position = position };
        data.nodeList.Add(node);
        return node;
    }
}
