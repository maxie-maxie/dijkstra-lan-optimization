using Xunit;

namespace dlo_winform.Tests;

public class SampleGraphsTests
{
    [Fact]
    public void Names_HasThirteen()
    {
        Assert.Equal(13, SampleGraphs.Names.Count);
    }

    [Fact]
    public void Names_FirstIsEmptyGraph()
    {
        Assert.Equal("Empty Graph", SampleGraphs.Names[0]);
    }

    [Fact]
    public void Names_AllNonEmpty()
    {
        foreach (var n in SampleGraphs.Names)
            Assert.False(string.IsNullOrWhiteSpace(n));
    }

    [Fact]
    public void OldSamples_PresentInOrder()
    {
        Assert.Equal("Linear 5", SampleGraphs.Names[1]);
        Assert.Equal("Star Hub", SampleGraphs.Names[2]);
        Assert.Equal("Ring 6", SampleGraphs.Names[3]);
        Assert.Equal("Mesh 7", SampleGraphs.Names[4]);
        Assert.Equal("Two Clusters", SampleGraphs.Names[5]);
        Assert.Equal("Weighted Detour", SampleGraphs.Names[6]);
        Assert.Equal("Disconnected Demo", SampleGraphs.Names[7]);
    }

    [Fact]
    public void ComplexSamples_PresentInOrder()
    {
        Assert.Equal("Grid 4x4", SampleGraphs.Names[8]);
        Assert.Equal("Diamond 54", SampleGraphs.Names[9]);
        Assert.Equal("Cluster 64", SampleGraphs.Names[10]);
        Assert.Equal("MeshWeb 76", SampleGraphs.Names[11]);
        Assert.Equal("Lattice 88", SampleGraphs.Names[12]);
    }

    [Fact]
    public void CreateAt_ReturnsFreshInstanceEachCall()
    {
        var a = SampleGraphs.CreateAt(0);
        var b = SampleGraphs.CreateAt(0);
        Assert.NotSame(a, b);
    }

    [Fact]
    public void CreateAt_NonEmptyIndicesReturnNonEmptyGraph()
    {
        for (int i = 1; i < SampleGraphs.Names.Count; i++)
        {
            var g = SampleGraphs.CreateAt(i);
            Assert.True(g.nodeList.Count > 0, $"Index {i} has no nodes");
            Assert.True(g.edgeList.Count > 0, $"Index {i} has no edges");
        }
    }

    [Fact]
    public void CreateAt_AllEdgesReferenceExistingNodes()
    {
        for (int i = 0; i < SampleGraphs.Names.Count; i++)
        {
            var g = SampleGraphs.CreateAt(i);
            var nodeSet = new HashSet<int>(g.nodeList.Select(n => n.Id));
            foreach (var e in g.edgeList)
            {
                Assert.Contains(e.StartNode.Id, nodeSet);
                Assert.Contains(e.EndNode.Id, nodeSet);
            }
        }
    }

    [Fact]
    public void CreateAt_EdgeWeightsArePositive()
    {
        for (int i = 0; i < SampleGraphs.Names.Count; i++)
        {
            var g = SampleGraphs.CreateAt(i);
            foreach (var e in g.edgeList)
                Assert.True(e.Weight > 0, $"Index {i} edge ({e.StartNode.Id}-{e.EndNode.Id}) weight {e.Weight}");
        }
    }

    [Fact]
    public void CreateAt_NodePositionsWithinCanvas()
    {
        for (int i = 0; i < SampleGraphs.Names.Count; i++)
        {
            var g = SampleGraphs.CreateAt(i);
            foreach (var n in g.nodeList)
            {
                Assert.True(n.Position.X >= 0, $"Index {i} node {n.Id} X={n.Position.X}");
                Assert.True(n.Position.X <= 640, $"Index {i} node {n.Id} X={n.Position.X}");
                Assert.True(n.Position.Y >= 0, $"Index {i} node {n.Id} Y={n.Position.Y}");
                Assert.True(n.Position.Y <= 420, $"Index {i} node {n.Id} Y={n.Position.Y}");
            }
        }
    }

    [Fact]
    public void ComplexSampleNodeCounts_InExpectedRanges()
    {
        int[][] ranges = { new[] { 10, 20 }, new[] { 50, 70 }, new[] { 60, 68 }, new[] { 70, 90 }, new[] { 80, 100 } };
        for (int i = 0; i < 5; i++)
        {
            int idx = 8 + i;
            var g = SampleGraphs.CreateAt(idx);
            Assert.True(g.nodeList.Count >= ranges[i][0], $"Index {idx} has {g.nodeList.Count} nodes, expected >= {ranges[i][0]}");
            Assert.True(g.nodeList.Count <= ranges[i][1], $"Index {idx} has {g.nodeList.Count} nodes, expected <= {ranges[i][1]}");
        }
    }

    [Fact]
    public void Cluster64_NodeIdsAreUnique()
    {
        var g = SampleGraphs.CreateAt(10);
        Assert.Equal(64, g.nodeList.Count);
        Assert.Equal(64, g.nodeList.Select(n => n.Id).Distinct().Count());
    }

    [Fact]
    public void Cluster64_UsesFullCanvas()
    {
        var g = SampleGraphs.CreateAt(10);
        bool hasRight = g.nodeList.Any(n => n.Position.X > 400);
        bool hasBottom = g.nodeList.Any(n => n.Position.Y > 250);
        Assert.True(hasRight, "Cluster64: no nodes in right half");
        Assert.True(hasBottom, "Cluster64: no nodes in bottom half");
    }

    [Fact]
    public void MeshWeb76_WeightsBetween20And100()
    {
        var g = SampleGraphs.CreateAt(11);
        Assert.Equal(76, g.nodeList.Count);
        foreach (var e in g.edgeList)
            Assert.True(e.Weight >= 20 && e.Weight <= 100,
                $"MeshWeb edge ({e.StartNode.Id}-{e.EndNode.Id}) weight {e.Weight} outside 20-100");
    }

    [Fact]
    public void DisconnectedDemo_HasUnreachable()
    {
        var g = SampleGraphs.CreateAt(7);
        Assert.False(DijkstraGraphService.FindRoute(g, 1, 4).Reachable);
    }

    [Fact]
    public void NextIndex_WrapsAround()
    {
        int last = SampleGraphs.Names.Count - 1;
        Assert.Equal(0, SampleGraphs.NextIndex(last));
        Assert.Equal(1, SampleGraphs.NextIndex(0));
    }

    [Fact]
    public void PreviousIndex_WrapsAround()
    {
        int last = SampleGraphs.Names.Count - 1;
        Assert.Equal(last, SampleGraphs.PreviousIndex(0));
        Assert.Equal(0, SampleGraphs.PreviousIndex(1));
    }

    [Fact]
    public void Grid16_AllNodesReachable()
    {
        var g = SampleGraphs.CreateAt(8);
        for (int s = 1; s <= 16; s++)
            for (int e = 1; e <= 16; e++)
            {
                if (s == e) continue;
                Assert.True(DijkstraGraphService.FindRoute(g, s, e).Reachable, $"Grid16: {s} -> {e}");
            }
    }
}
