namespace dlo_winform;

public static class GraphEditor
{
    public static void AddEdgeIfMissing(GraphData data, NetworkNode startNode, NetworkNode endNode, int weight)
    {
        if (startNode == null || endNode == null || startNode == endNode) return;

        // Check if edge already exists (undirected)
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
}
