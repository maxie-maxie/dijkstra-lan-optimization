using System.Drawing;
using System.Collections.Generic;

namespace dlo_winform;

public static class RandomGraphGenerator
{
    public static GraphData Generate(int nodeCount, Size canvasSize)
    {
        var data = new GraphData();
        var random = new Random();

        // Create nodes with random positions
        for (int i = 1; i <= nodeCount; i++)
        {
            int margin = 30;
            float x = random.Next(margin, canvasSize.Width - margin);
            float y = random.Next(margin, canvasSize.Height - margin);
            data.nodeList.Add(new NetworkNode { Id = i, Position = new PointF(x, y) });
        }

        // Ensure graph is connected - create a path 1->2->3->...->N
        for (int i = 0; i < nodeCount - 1; i++)
        {
            int weight = random.Next(1, 21); // Weight 1-20
            data.edgeList.Add(new NetworkEdge
            {
                StartNode = data.nodeList[i],
                EndNode = data.nodeList[i + 1],
                Weight = weight
            });
        }

        // Add extra random edges (about nodeCount extra edges)
        int extraEdges = nodeCount;
        for (int i = 0; i < extraEdges; i++)
        {
            int fromIndex = random.Next(nodeCount);
            int toIndex = random.Next(nodeCount);

            if (fromIndex == toIndex) continue; // Skip self-edges

            var fromNode = data.nodeList[fromIndex];
            var toNode = data.nodeList[toIndex];

            // Check if edge already exists
            bool exists = data.edgeList.Any(edge =>
                (edge.StartNode == fromNode && edge.EndNode == toNode) ||
                (edge.StartNode == toNode && edge.EndNode == fromNode));

            if (!exists)
            {
                int weight = random.Next(1, 21);
                data.edgeList.Add(new NetworkEdge
                {
                    StartNode = fromNode,
                    EndNode = toNode,
                    Weight = weight
                });
            }
        }

        return data;
    }
}
