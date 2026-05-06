using Xunit;
using System.Drawing;

namespace dlo_winform.Tests;

public class GraphEditorAdvancedTests
{
    [Fact]
    public void ToggleEdge_ReturnsCreatedWhenMissing()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var b = new NetworkNode { Id = 2, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });

        var outcome = GraphEditor.ToggleEdge(data, a, b, 125_000_000L);

        Assert.Equal(ToggleEdgeOutcome.Created, outcome);
        Assert.Single(data.edgeList);
    }

    [Fact]
    public void ToggleEdge_ReturnsRemovedWhenExists()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var b = new NetworkNode { Id = 2, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5, TransferSpeedBytesPerSecond = 125_000_000L });

        var outcome = GraphEditor.ToggleEdge(data, a, b, 125_000_000L);

        Assert.Equal(ToggleEdgeOutcome.Removed, outcome);
        Assert.Empty(data.edgeList);
    }

    [Fact]
    public void ToggleEdge_ReturnsNoActionForSameNode()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.Add(a);

        var outcome = GraphEditor.ToggleEdge(data, a, a, 125_000_000L);

        Assert.Equal(ToggleEdgeOutcome.NoAction, outcome);
        Assert.Empty(data.edgeList);
    }

    [Fact]
    public void RemoveNode_RemovesNodeAndConnectedEdges()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(50, 50) };
        var b = new NetworkNode { Id = 2, Position = new PointF(100, 100) };
        var c = new NetworkNode { Id = 3, Position = new PointF(200, 200) };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b, c });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = c, Weight = 3 });

        var (nodeRemoved, removedCount, _) = GraphEditor.RemoveNode(data, new PointF(55, 55));

        Assert.True(nodeRemoved);
        Assert.Equal(2, removedCount);
        Assert.Equal(2, data.nodeList.Count);
        Assert.Empty(data.edgeList);
    }

    [Fact]
    public void RemoveNode_RemovesOnlyConnectedEdges()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(50, 50) };
        var b = new NetworkNode { Id = 2, Position = new PointF(100, 100) };
        var c = new NetworkNode { Id = 3, Position = new PointF(200, 200) };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b, c });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });
        data.edgeList.Add(new NetworkEdge { StartNode = b, EndNode = c, Weight = 3 });

        var (nodeRemoved, removedCount, _) = GraphEditor.RemoveNode(data, new PointF(55, 55));

        Assert.True(nodeRemoved);
        Assert.Equal(1, removedCount);
        Assert.Equal(2, data.nodeList.Count);
        Assert.Single(data.edgeList);
        Assert.Equal(b, data.edgeList[0].StartNode);
        Assert.Equal(c, data.edgeList[0].EndNode);
    }

    [Fact]
    public void RemoveNode_RemovesNodeWithNoEdges()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(50, 50) };
        var data = new GraphData();
        data.nodeList.Add(a);

        var (nodeRemoved, removedCount, _) = GraphEditor.RemoveNode(data, new PointF(55, 55));

        Assert.True(nodeRemoved);
        Assert.Equal(0, removedCount);
        Assert.Empty(data.nodeList);
    }

    [Fact]
    public void RemoveNode_NodeNotFound_ReturnsZero()
    {
        var data = new GraphData();
        data.nodeList.Add(new NetworkNode { Id = 1, Position = new PointF(50, 50) });

        var (nodeRemoved, removedCount, _) = GraphEditor.RemoveNode(data, new PointF(999, 999));

        Assert.False(nodeRemoved);
        Assert.Equal(0, removedCount);
        Assert.Single(data.nodeList);
    }

    [Fact]
    public void GenerateTransferSpeed_ReturnsPositiveInRange()
    {
        for (int i = 0; i < 100; i++)
        {
            long speed = GraphEditor.GenerateTransferSpeed();
            Assert.True(speed > 0, $"Speed should be positive, got {speed}");
            Assert.True(speed <= 2_000_000_000L, $"Speed should be <= 2 GB/s, got {speed}");
        }
    }

    [Fact]
    public void GenerateTransferSpeed_ProducesVariation()
    {
        var speeds = new HashSet<long>();
        for (int i = 0; i < 50; i++)
            speeds.Add(GraphEditor.GenerateTransferSpeed());
        Assert.True(speeds.Count > 5, "Should produce varied speeds, got only " + speeds.Count + " unique");
    }

    [Fact]
    public void PacketSizeBytes_CalculatesCorrectly()
    {
        Assert.Equal(1L, GraphEditor.PacketSizeBytes(1, PacketUnit.Bytes));
        Assert.Equal(1024L, GraphEditor.PacketSizeBytes(1, PacketUnit.KB));
        Assert.Equal(1024L * 1024, GraphEditor.PacketSizeBytes(1, PacketUnit.MB));
        Assert.Equal(1024L * 1024 * 1024, GraphEditor.PacketSizeBytes(1, PacketUnit.GB));
    }

    [Fact]
    public void PacketSizeBytes_ZeroReturnsZero()
    {
        Assert.Equal(0L, GraphEditor.PacketSizeBytes(0, PacketUnit.Bytes));
    }

    [Fact]
    public void ComputeEdgeWeight_ReturnsOneForSubMsTransfer()
    {
        long packetBytes = 1500;
        long speedBytesPerSec = 125_000_000L; // ~1 Gbps
        int weightMs = GraphEditor.ComputeEdgeWeightMs(packetBytes, speedBytesPerSec);
        // 1500 / 125000000 * 1000 = 0.012ms, floor at 1
        Assert.Equal(1, weightMs);
    }

    [Fact]
    public void ComputeEdgeWeight_LargePacketReturnsMs()
    {
        long packetBytes = 10L * 1024 * 1024; // 10 MB
        long speedBytesPerSec = 125_000_000L; // ~1 Gbps = 125 MB/s
        int weightMs = GraphEditor.ComputeEdgeWeightMs(packetBytes, speedBytesPerSec);
        long expected = (long)(10.0 * 1024 * 1024 / 125_000_000.0 * 1000.0);
        Assert.Equal(expected, weightMs);
    }

    [Fact]
    public void ToggleEdge_SetsSpeedOnCreatedEdge()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var b = new NetworkNode { Id = 2, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });
        long speed = 125_000_000L;

        GraphEditor.ToggleEdge(data, a, b, speed);

        var edge = data.edgeList[0];
        Assert.Equal(speed, edge.TransferSpeedBytesPerSecond);
    }

    [Fact]
    public void Dijkstra_UsesRecalculatedWeightsFromSpeed()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(10, 0) };
        var c = new NetworkNode { Id = 3, Position = new PointF(20, 0) };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b, c });
        var ab = new NetworkEdge { StartNode = a, EndNode = b, Weight = 0, TransferSpeedBytesPerSecond = 125_000_000L };
        var bc = new NetworkEdge { StartNode = b, EndNode = c, Weight = 0, TransferSpeedBytesPerSecond = 125_000_000L };
        var ac = new NetworkEdge { StartNode = a, EndNode = c, Weight = 0, TransferSpeedBytesPerSecond = 10_000_000L };
        data.edgeList.AddRange(new[] { ab, bc, ac });

        long packetBytes = 10L * 1024 * 1024; // 10 MB — big enough to produce measurable ms
        GraphEditor.RecalculateEdgeWeights(data, packetBytes);

        var route = DijkstraGraphService.FindRoute(data, 1, 3);
        Assert.True(route.Reachable);
        // 1->2->3 via 125 MB/s links = ~167 ms total
        // 1->3 direct via 10 MB/s link = ~1048 ms total
        Assert.Equal(new List<int> { 1, 2, 3 }, route.PathNodeIds);
    }

    [Fact]
    public void RecalculateEdgeWeights_AllEdgesUpdated()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(10, 0) };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 999, TransferSpeedBytesPerSecond = 125_000_000L });

        GraphEditor.RecalculateEdgeWeights(data, 1500);

        Assert.NotEqual(999, data.edgeList[0].Weight);
        Assert.True(data.edgeList[0].Weight >= 0);
    }

    [Fact]
    public void PacketUnit_EnumValuesAreCorrect()
    {
        Assert.Equal(0, (int)PacketUnit.Bytes);
        Assert.Equal(1, (int)PacketUnit.KB);
        Assert.Equal(2, (int)PacketUnit.MB);
        Assert.Equal(3, (int)PacketUnit.GB);
    }
}
