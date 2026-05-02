using Xunit;
using System.Drawing;
using dlo_winform;

namespace dlo_winform.Tests;

public class RandomGraphGeneratorTests
{
    [Fact]
    public void GenerateTree_CreatesExactlyNodeCountMinusOneEdges()
    {
        var result = RandomGraphGenerator.GenerateTree(10, new Size(500, 500), new Random(42));

        Assert.Equal(9, result.edgeList.Count);
    }

    [Fact]
    public void GenerateTree_DoesNotCreateOverlappingNodes()
    {
        var result = RandomGraphGenerator.GenerateTree(10, new Size(500, 500), new Random(42));
        int radius = 10;
        int minSpacing = radius * 2 + 8;

        for (int i = 0; i < result.nodeList.Count; i++)
        {
            for (int j = i + 1; j < result.nodeList.Count; j++)
            {
                float dist = GeometryHelpers.Distance(
                    result.nodeList[i].Position, result.nodeList[j].Position);
                Assert.True(dist >= minSpacing - 0.01f,
                    $"Nodes {result.nodeList[i].Id} and {result.nodeList[j].Id} are too close: {dist:F2} < {minSpacing}");
            }
        }
    }

    [Fact]
    public void GenerateTree_AllNodesAreReachable()
    {
        var result = RandomGraphGenerator.GenerateTree(10, new Size(500, 500), new Random(42));
        var route = DijkstraGraphService.FindRoute(result, 1, 10);

        Assert.True(route.Reachable);
    }

    [Fact]
    public void GenerateTree_AllWeightsArePositive()
    {
        var result = RandomGraphGenerator.GenerateTree(10, new Size(500, 500), new Random(42));

        foreach (var edge in result.edgeList)
        {
            Assert.True(edge.Weight > 0, $"Edge ({edge.StartNode.Id}-{edge.EndNode.Id}) has non-positive weight {edge.Weight}");
        }
    }
}
