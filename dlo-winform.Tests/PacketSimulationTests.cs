using Xunit;
using dlo_winform;
using System.Drawing;

namespace dlo_winform.Tests;

public class PacketSimulationTests
{
    [Fact]
    public void Tick_MovesPacketOneEdgeAndAddsEdgeWeightToElapsedTime()
    {
        var a = new NetworkNode { Id = 1 };
        var b = new NetworkNode { Id = 2 };
        var edge = new NetworkEdge { StartNode = a, EndNode = b, Weight = 7 };

        var route = new DijkstraRouteResult
        {
            StartNodeId = 1,
            DestinationNodeId = 2,
            Reachable = true,
            TotalTime = 7,
            PathNodeIds = new List<int> { 1, 2 },
            PathEdges = new List<NetworkEdge> { edge }
        };

        var simulation = new PacketSimulation(route);
        var tick = simulation.Tick();

        Assert.True(tick.IsMove);
        Assert.Equal(1, tick.FromNodeId);
        Assert.Equal(2, tick.ToNodeId);
        Assert.Equal(7, tick.TickTravelTime);
        Assert.Equal(7, tick.TotalElapsedTime);
    }

    [Fact]
    public void Tick_ReturnsCompletedWhenAllEdgesTraversed()
    {
        var a = new NetworkNode { Id = 1 };
        var b = new NetworkNode { Id = 2 };
        var edge = new NetworkEdge { StartNode = a, EndNode = b, Weight = 7 };

        var route = new DijkstraRouteResult
        {
            StartNodeId = 1,
            DestinationNodeId = 2,
            Reachable = true,
            TotalTime = 7,
            PathNodeIds = new List<int> { 1, 2 },
            PathEdges = new List<NetworkEdge> { edge }
        };

        var simulation = new PacketSimulation(route);
        simulation.Tick(); // First tick - moves
        var tick = simulation.Tick(); // Second tick - should be completed

        Assert.True(tick.IsComplete);
        Assert.Equal(7, tick.TotalElapsedTime);
    }
}
