using System.Drawing;

namespace dlo_winform;

public static class GeometryHelpers
{
    public static bool CirclesOverlap(PointF a, float radiusA, PointF b, float radiusB)
    {
        float dx = a.X - b.X;
        float dy = a.Y - b.Y;
        float distSq = dx * dx + dy * dy;
        float radiusSum = radiusA + radiusB;
        return distSq < radiusSum * radiusSum;
    }

    public static bool SegmentsIntersect(PointF a1, PointF a2, PointF b1, PointF b2)
    {
        float orientation(PointF p, PointF q, PointF r)
        {
            return (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
        }

        float o1 = orientation(a1, a2, b1);
        float o2 = orientation(a1, a2, b2);
        float o3 = orientation(b1, b2, a1);
        float o4 = orientation(b1, b2, a2);

        if (o1 == 0 && IsOnSegment(a1, a2, b1)) return true;
        if (o2 == 0 && IsOnSegment(a1, a2, b2)) return true;
        if (o3 == 0 && IsOnSegment(b1, b2, a1)) return true;
        if (o4 == 0 && IsOnSegment(b1, b2, a2)) return true;

        return (o1 > 0) != (o2 > 0) && (o3 > 0) != (o4 > 0);
    }

    private static bool IsOnSegment(PointF a, PointF b, PointF c)
    {
        return c.X >= Math.Min(a.X, b.X) && c.X <= Math.Max(a.X, b.X) &&
               c.Y >= Math.Min(a.Y, b.Y) && c.Y <= Math.Max(a.Y, b.Y);
    }

    public static float DistanceToSegment(PointF p, PointF a, PointF b)
    {
        float dx = b.X - a.X;
        float dy = b.Y - a.Y;
        float lengthSq = dx * dx + dy * dy;
        if (lengthSq == 0) return Distance(p, a);

        float t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / lengthSq;
        t = Math.Clamp(t, 0f, 1f);

        float projX = a.X + t * dx;
        float projY = a.Y + t * dy;
        return Distance(p, new PointF(projX, projY));
    }

    public static float Distance(PointF a, PointF b)
    {
        float dx = a.X - b.X;
        float dy = a.Y - b.Y;
        return MathF.Sqrt(dx * dx + dy * dy);
    }

    public static bool EdgePassesThroughNode(NetworkEdge edge, NetworkNode node)
    {
        float dist = DistanceToSegment(node.Position, edge.StartNode.Position, edge.EndNode.Position);
        return dist < node.Radius;
    }
}
