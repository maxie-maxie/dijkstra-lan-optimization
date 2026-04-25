using System.Numerics;
using Tree;

namespace Graph
{
    public class MatrixGraph
    {
        const long INF = 1000000000000000000L;
        private int vertex;
        private long[,] adjMat;
        private long[] distance;
        private int[] trace;
        public MatrixGraph(int _vertex = 0)
        {
            vertex = _vertex;
            adjMat = new long[vertex + 1, vertex + 1];
            distance =new long[vertex + 1];
            trace =new int[vertex + 1];
            for (int i = 0; i <= vertex; i++)
                for (int j = 0; j <= vertex; j++) adjMat[i, j] = i == j ? 0 : INF;
            for (int i = 0; i < distance.Length; i++) distance[i] = INF;
            for (int i = 0; i < trace.Length; i++) trace[i] = -1;
        }
        public void AddEdge(int from, int to, long weight, bool isDirected = false)
        {
            adjMat[from, to] = Math.Min(adjMat[from, to], weight);
            if (!isDirected) adjMat[to, from] = Math.Min(adjMat[to, from], weight);
        }
        private void Reset()
        {
            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = INF;
                trace[i] = -1;
            }
        }
        public long[] Dijkstra(int start)
        {
            Reset();
            distance[start] = 0;
            bool[] visited = new bool[vertex + 1];
            for (int i = 0; i <= vertex; i++)
            {
                int u = -1;
                for (int j = 0; j <= vertex; j++) if (!visited[j] && (u == -1 || distance[j] < distance[u])) u = j;
                if (u == -1 || distance[u] == INF) break;
                visited[u] = true;
                for (int v = 0; v <= vertex; v++)
                {
                    if (!visited[v] && adjMat[u, v] != INF && distance[u] + adjMat[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + adjMat[u, v];
                        trace[v] = u;
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
    public struct Edge
    {
        public int to;
        public long weight;
    }
    public class EdgeGraph
    {
        const long INF = 1000000000000000000L;
        private int vertex, edge;
        private List<List<Edge>> adjList;
        private long[] distance;
        private int[] trace;
        MinHeap myHeap;
        public EdgeGraph(int _vertex = 0)
        {
            vertex = _vertex;
            edge = 0;
            adjList = new List<List<Edge>>();
            distance = new long[vertex + 1];
            trace = new int[vertex + 1];
            myHeap = new MinHeap();
            for (int i = 0; i <= vertex; i++) adjList.Add(new List<Edge>());
            for (int i = 0; i < distance.Length; i++) distance[i] = INF;
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
        private void SparseDijkstra(int start)
        {
            distance[start] = 0;
            myHeap.Enqueue(start, distance[start]);
            while (!myHeap.IsEmpty())
            {
                int u = myHeap.PeekVertex();
                long distance_u = myHeap.PeekWeight();
                myHeap.Dequeue();
                if (distance_u != distance[u]) continue;
                foreach (Edge edge in adjList[u])
                {
                    int v = edge.to;
                    long length = edge.weight;
                    if (distance[u] + length < distance[v])
                    {
                        distance[v] = distance[u] + length;
                        trace[v] = u;
                        myHeap.Enqueue(v, distance[v]);
                    }
                }
            }
        }
        private void DenseDijkstra(int start)
        {
            distance[start] = 0;
            bool[] from = new bool[vertex + 1];
            for (int i = 0; i <= vertex; i++)
            {
                int v = -1;
                for (int j = 0; j <= vertex; j++) if (!from[j] && (v == -1 || distance[j] < distance[v])) v = j;
                if (distance[v] == INF) break;
                from[v] = true;
                foreach (Edge e in adjList[v])
                {
                    int to = e.to;
                    long length = e.weight;
                    if (distance[v] + length < distance[to])
                    {
                        distance[to] = distance[v] + length;
                        trace[to] = v;
                    }
                }
            }
        }
        private void Reset()
        {
            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = INF;
                trace[i] = -1;
            }
        }
        public long[] Dijkstra(int start)
        {
            Reset();
            int logV = BitOperations.Log2((uint)vertex);
            if (1L * (vertex + edge) * logV <= 1L * vertex * vertex + edge) SparseDijkstra(start);
            else DenseDijkstra(start);
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
}
