using Graph;
using FastInputOutput;

namespace Workspace
{
    internal class Program
    {
        static void Weighted()
        {
            Scanner sc = new Scanner();
            int vertex = sc.NextInt32(), edge = sc.NextInt32();
            EdgeGraph wg = new EdgeGraph(vertex);

            for (int i = 0; i < edge; i++)
            {
                int u = sc.NextInt32(), v = sc.NextInt32();
                long w = sc.NextInt64();
                wg.AddEdge(u, v, w, true);
            }

            long[] distance = wg.Dijkstra(1);
            for (int i = 1; i < distance.Length; i++) 
            {
                if (distance[i] < wg.Infinity) Console.Write(distance[i] + " ");
                else Console.Write("-1 ");
            }
        }
        static void Main()
        {
            Console.Clear();

            Weighted();
        }
    }
}