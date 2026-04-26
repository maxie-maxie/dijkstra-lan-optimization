using Graph;
using FastInputOutput;

namespace Workspace
{
    internal class Program
    {
        static Scanner sc = new Scanner();
        static void Weighted()
        {
            Console.SetIn(new StreamReader("input2.txt"));
            int vertex = sc.NextInt32(), edge = sc.NextInt32();
            MatrixGraph wg = new MatrixGraph(vertex);
            for (int i = 0; i < edge; i++)
            {
                int u = sc.NextInt32(), v = sc.NextInt32();
                long w = sc.NextInt64();
                wg.AddEdge(u, v, w, true);
            }
            long[] distance = wg.Dijkstra(1);
            using (StreamWriter sw = new StreamWriter("output.txt", false, System.Text.Encoding.ASCII, 65536))
            {
                for (int i = 1; i <= vertex; i++)
                {
                    if (distance[i] < wg.Infinity)
                    {
                        sw.Write(distance[i]); 
                        sw.Write(" ");
                    }
                    else sw.Write("-1 ");
                }
            }
        }
        static void SPSP()
        {
            int vertex = sc.NextInt32(), edge = sc.NextInt32(), start = sc.NextInt32(), end = sc.NextInt32();
            EdgeGraph myGraph = new EdgeGraph(vertex);
            for (int i = 0; i < edge; i++)
            {
                int u = sc.NextInt32(), v = sc.NextInt32();
                long w = sc.NextInt64();
                myGraph.AddEdge(u, v, w);
            }
            long[] distance = myGraph.Dijkstra(start);
            List<int> path = myGraph.tracePath(start, end);
            if (path.Count == 0)
            {
                Console.Write("Not Found.");
                return;
            }
            Console.Write(distance[end]);
            Console.Write("\n");
            foreach(int i in path)
            {
                Console.Write(i);
                Console.Write(" ");
            }
        }
        static void Main()
        {
            //Weighted();
            SPSP();
        }
    }
}