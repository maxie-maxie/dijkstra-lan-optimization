using Xunit;
using System.Drawing;
using dlo_winform;

namespace dlo_winform.Tests;

public class GeometryHelpersTests
{
    [Fact]
    public void CirclesOverlap_ReturnsTrueWhenDistanceIsLessThanRadiusSum()
    {
        Assert.True(GeometryHelpers.CirclesOverlap(new PointF(0, 0), 10, new PointF(15, 0), 10));
    }

    [Fact]
    public void CirclesOverlap_ReturnsFalseWhenDistanceIsMoreThanRadiusSum()
    {
        Assert.False(GeometryHelpers.CirclesOverlap(new PointF(0, 0), 10, new PointF(30, 0), 10));
    }

    [Fact]
    public void SegmentsIntersect_ReturnsTrueForCrossingLines()
    {
        Assert.True(GeometryHelpers.SegmentsIntersect(
            new PointF(0, 0),
            new PointF(10, 10),
            new PointF(0, 10),
            new PointF(10, 0)));
    }

    [Fact]
    public void SegmentsIntersect_ReturnsFalseForParallelLines()
    {
        Assert.False(GeometryHelpers.SegmentsIntersect(
            new PointF(0, 0),
            new PointF(10, 0),
            new PointF(0, 10),
            new PointF(10, 10)));
    }

    [Fact]
    public void DistanceToSegment_PointOnSegment_ZeroDistance()
    {
        float dist = GeometryHelpers.DistanceToSegment(new PointF(5, 0), new PointF(0, 0), new PointF(10, 0));
        Assert.Equal(0f, dist);
    }

    [Fact]
    public void DistanceToSegment_PointOffSegment_ReturnsPerpendicularDistance()
    {
        float dist = GeometryHelpers.DistanceToSegment(new PointF(5, 5), new PointF(0, 0), new PointF(10, 0));
        Assert.Equal(5f, dist, precision: 2);
    }

    [Fact]
    public void EdgePassesThroughNode_ReturnsTrueForNodeOnEdgeMidpoint()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(10, 0) };
        var edge = new NetworkEdge { StartNode = a, EndNode = b };
        var node = new NetworkNode { Id = 3, Position = new PointF(5, 0), Radius = 3 };

        Assert.True(GeometryHelpers.EdgePassesThroughNode(edge, node));
    }
}
