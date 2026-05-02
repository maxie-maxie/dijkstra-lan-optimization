namespace dlo_winform;

public sealed class PacketTickResult
{
    public bool IsMove { get; }
    public bool IsComplete { get; }
    public int FromNodeId { get; }
    public int ToNodeId { get; }
    public long TickTravelTime { get; }
    public long TotalElapsedTime { get; }

    private PacketTickResult(bool isMove, bool isComplete, int fromNodeId, int toNodeId, long tickTravelTime, long totalElapsedTime)
    {
        IsMove = isMove;
        IsComplete = isComplete;
        FromNodeId = fromNodeId;
        ToNodeId = toNodeId;
        TickTravelTime = tickTravelTime;
        TotalElapsedTime = totalElapsedTime;
    }

    public static PacketTickResult Moved(int fromNodeId, int toNodeId, long tickTravelTime, long totalElapsedTime)
    {
        return new PacketTickResult(true, false, fromNodeId, toNodeId, tickTravelTime, totalElapsedTime);
    }

    public static PacketTickResult Completed(long totalElapsedTime)
    {
        return new PacketTickResult(false, true, 0, 0, 0, totalElapsedTime);
    }
}

public sealed class PacketSimulation
{
    public DijkstraRouteResult Route { get; }
    public int CurrentEdgeIndex { get; private set; } = -1;
    public long ElapsedTime { get; private set; }
    public bool IsComplete => CurrentEdgeIndex >= Route.PathEdges.Count - 1;

    public PacketSimulation(DijkstraRouteResult route)
    {
        Route = route;
    }

    public PacketTickResult Tick()
    {
        if (IsComplete)
            return PacketTickResult.Completed(ElapsedTime);

        CurrentEdgeIndex++;

        if (CurrentEdgeIndex >= Route.PathEdges.Count)
            return PacketTickResult.Completed(ElapsedTime);

        NetworkEdge edge = Route.PathEdges[CurrentEdgeIndex];
        ElapsedTime += edge.Weight;

        return PacketTickResult.Moved(edge.StartNode.Id, edge.EndNode.Id, edge.Weight, ElapsedTime);
    }
}
