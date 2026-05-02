namespace dlo_winform;

static class GraphProcessing
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
//Data models
public class NetworkNode 
{
    public int Id { get; set; } 
    public PointF Position { get; set; } 
    public int Radius { get; set; } = 20; 
}

public class NetworkEdge 
{
    public NetworkNode StartNode { get; set; } 
    public NetworkNode EndNode { get; set; } 
    public int Weight { get; set; } 
}

//DB
public class GraphData
{    
    public List<NetworkNode> nodeList;
    public List<NetworkEdge> edgeList;
    
    public NetworkNode GetNode(Point mousePos)
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
}

