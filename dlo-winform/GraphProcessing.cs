namespace dlo_winform;

public class NetworkNode 
{
    public int Id { get; set; } 
    public PointF Position { get; set; } 
    public int Radius { get; set; } = 10; 
}

public class NetworkEdge 
{
    public NetworkNode StartNode { get; set; } = null!;
    public NetworkNode EndNode { get; set; } = null!;
    public int Weight { get; set; }
    public long TransferSpeedBytesPerSecond { get; set; }
}

public class GraphData
{    
    public List<NetworkNode> nodeList { get; } = new();
    public List<NetworkEdge> edgeList { get; } = new();
    
    public NetworkNode? GetNode(Point mousePos)
    {
        foreach (NetworkNode node in nodeList)
        {
            double vectorX = mousePos.X - node.Position.X;
            double vectorY = mousePos.Y - node.Position.Y;
            double distance = Math.Sqrt(vectorX * vectorX + vectorY * vectorY);
            if (distance <= node.Radius) return node;
        }
        return null;
    }

    public NetworkNode? GetNodeById(int id)
    {
        foreach (var node in nodeList)
        {
            if (node.Id == id) return node;
        }
        return null;
    }
}

public enum ToggleEdgeOutcome
{
    Created,
    Removed,
    NoAction
}

public enum PacketUnit
{
    Bytes,
    KB,
    MB,
    GB
}
