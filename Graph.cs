using System.Numerics;
using Tree;

namespace Graph
{
    public class MatrixGraph
    {
        const long INF = 1000000000000000000L;
        private int vertex;
        private long[,] adjMat;
        int[] trace;
        public MatrixGraph(int _vertex = 0)
        {
            vertex = _vertex;
            adjMat = new long[vertex + 1, vertex + 1];
            trace = new int[vertex + 1];
            for (int i = 0; i <= vertex; i++)
                for (int j = 0; j <= vertex; j++) adjMat[i, j] = i == j ? 0 : INF;
            for (int i = 0; i < trace.Length; i++) trace[i] = -1;
        }
        public long Infinity
        {
            get
            {
                return INF;
            }
        }
        public void AddEdge(int from, int to, long weight, bool isDirected = false)
        {
            adjMat[from, to] = Math.Min(adjMat[from, to], weight);
            if (!isDirected) adjMat[to, from] = Math.Min(adjMat[to, from], weight);
        }
        public long[] Dijkstra(int start)
        {
            long[] distance = new long[vertex + 1];
            for (int i = 0; i < distance.Length; i++) distance[i] = INF;
            bool[] visited = new bool[vertex + 1];
            distance[start] = 0;
            for (int i = 0; i <= vertex; i++)
            {
                int from = -1;
                for (int j = 0; j <= vertex; j++) if (!visited[j] && (from == -1 || distance[j] < distance[from])) from = j;
                if (from == -1 || distance[from] == INF) break;
                visited[from] = true;
                for (int to = 0; to <= vertex; to++)
                {
                    if (!visited[to] && adjMat[from, to] != INF && distance[from] + adjMat[from, to] < distance[to])
                    {
                        distance[to] = distance[from] + adjMat[from, to];
                        trace[to] = from;
                    }
                }
            }
            return distance;
        }
        public List<int> tracePath(int start, int end)
        {
            if (end != start && trace[end] == -1) return new List<int>();
            List<int> path = new List<int>();
            while (end != -1)
            {
                path.Add(end);
                end = trace[end];
            }
            path.Reverse();
            return path;
        }
    }
    public class EdgeGraph
    {
        public struct Edge
        {
            public int to;
            public long weight;
        }
        const long INF = 1000000000000000000L;
        private int vertex, edge;
        private List<Edge>[] adjList;
        private int[] trace;
        public EdgeGraph(int _vertex = 0)
        {
            vertex = _vertex;
            edge = 0;
            adjList = new List<Edge>[vertex + 1];
            trace = new int[vertex + 1];
            for (int i = 0; i <= vertex; i++) adjList[i] = new List<Edge>();
            for (int i = 0; i < trace.Length; i++) trace[i] = -1;
        }
        public long Infinity
        {
            get
            {
                return INF;
            }
        }
        public void AddEdge(int _from, int _to, long _weight, bool isDirected = false)
        {
            Edge e = new Edge()
            {
                to = _to,
                weight = _weight
            };
            adjList[_from].Add(e);
            edge++;
            if (!isDirected)
            {
                e.to = _from;
                adjList[_to].Add(e);
                edge++;
            }
        }
        public long[] DenseDijkstra(int start)
        {
            long[] distance = new long[vertex + 1];
            for (int i = 0; i < distance.Length; i++) distance[i] = INF;
            distance[start] = 0;
            bool[] visited = new bool[vertex + 1];
            for (int i = 0; i <= vertex; i++)
            {
                int from = -1;
                for (int j = 0; j <= vertex; j++) if (!visited[j] && (from == -1 || distance[j] < distance[from])) from = j;
                if (distance[from] == INF) break;
                visited[from] = true;
                foreach (Edge edge in adjList[from])
                {
                    int to = edge.to;
                    long length = edge.weight;
                    if (distance[from] + length < distance[to])
                    {
                        distance[to] = distance[from] + length;
                        trace[to] = from;
                    }
                }
            }
            return distance;
        }
        public long[] SparseDijkstra(int start)
        {
            long[] distance = new long[vertex + 1];
            for (int i = 0; i < distance.Length; i++) distance[i] = INF;
            distance[start] = 0;
            MinHeap myHeap = new MinHeap();
            myHeap.Enqueue(start, distance[start]);
            while (!myHeap.IsEmpty())
            {
                int from = myHeap.PeekVertex();
                long distance_from = myHeap.PeekWeight();
                myHeap.Dequeue();
                if (distance_from != distance[from]) continue;
                foreach (Edge edge in adjList[from])
                {
                    int to = edge.to;
                    long length = edge.weight;
                    if (distance[from] + length < distance[to])
                    {
                        distance[to] = distance[from] + length;
                        trace[to] = from;
                        myHeap.Enqueue(to, distance[to]);
                    }
                }
            }
            return distance;
        }
        public long[] Dijkstra(int start) // It is only good for a Graph which has only 1 Connected Component.
        {
            int logV = BitOperations.Log2((uint)vertex);
            if (1L * (vertex + edge) * logV <= 1L * vertex * vertex + edge) return SparseDijkstra(start);
            else return DenseDijkstra(start);
        }
        public List<int> tracePath(int start, int end)
        {
            if (end != start && trace[end] == -1) return new List<int>();
            List<int> path = new List<int>();
            while (end != -1)
            {
                path.Add(end);
                end = trace[end];
            }
            path.Reverse();
            return path;
        }
    }
}
