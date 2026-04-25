#include <iostream>
#include <algorithm>
#include <vector>
#include <queue>
#include <map>
using std::string;
using std::vector;
using std::pair;
using std::queue;
using std::map;
using std::cout;

struct Vertex {
    bool visited;
    string label;
    Vertex(string _label = "") {
        visited = 0;
        label = _label;
    }
};

struct Edge {
    int to;
    long long weight;
};

struct Node {
    int vertex;
    long long distance;

    bool operator>(const Node& other) const {
        return distance > other.distance;
    }
};

struct Graph {
    const long long INF = (long long) 1e18;

    vector<Vertex> vertices;
    vector<vector<Edge>> adjList;
    vector<map<int, long long>> sortedAdj;
    vector<long long> distance;
    vector<int> trace;

    Graph(int vertex = 0) {
        vertices.resize(vertex + 1);
        adjList.resize(vertex + 1);
        sortedAdj.resize(vertex + 1);
        distance.resize(vertex + 1, INF);
        trace.resize(vertex + 1, -1);
    }
    
    void addVertex(string label = "") {
        vertices.push_back(Vertex(label));
        adjList.push_back(vector<Edge>());
        sortedAdj.push_back(map<int, long long>());
        distance.push_back(INF);
        trace.push_back(0);
    }

    void addEdge(int start, int end, long long weight = 1) {
        adjList[start].push_back({end, weight});
        adjList[end].push_back({start, weight});

        sortedAdj[start].insert({end, weight});
        sortedAdj[end].insert({start, weight});
    }

    void addDirectedEdge(int start, int end, long long weight = 1) {
        adjList[start].push_back({end, weight});
        sortedAdj[start].insert({end, weight});
    }

    void printVertex(int u) {
        cout << vertices[u].label << ' ';
    }

    void printEdge(int u, int v) {
        if (sortedAdj[u].count(v)) cout << vertices[u].label << ' ' << vertices[v].label << '\n';
    }

    void printAllEdges() {
        for (int u = 0; u < vertices.size(); u++) {
            if (adjList[u].empty()) continue;
            for (const Edge &v : adjList[u]) cout << vertices[u].label << ' ' << vertices[v.to].label << '\n';
        }
    }

    void printWeightedEdge(int u, int v) {
        long long length = sortedAdj[u][v];
        if (length) cout << vertices[u].label << ' ' << vertices[v].label << ' ' << length << '\n';
    }

    void printAllWeightedEdges() {
        for (int u = 0; u < vertices.size(); u++) {
            if (adjList[u].empty()) continue;
            for (const Edge &v : adjList[u]) cout << vertices[u].label << ' ' << vertices[v.to].label << ' ' << v.weight << '\n';
        }
    }

    void dfs(int u, bool print = 0) {
        if (vertices[u].visited) return;
        vertices[u].visited = 1;
        if (print) printVertex(u);
        for (const Edge &v : adjList[u]) dfs(v.to, print);
    }

    void bfs(int start, bool print = 0) {
        if (!vertices[start].visited) {
            queue<int> q;
            q.push(start);
            vertices[start].visited = 1;

            while (!q.empty()) {
                int u = q.front();
                q.pop();
                if (print) printVertex(u);

                for (const Edge &v : adjList[u]) if (!vertices[v.to].visited) {
                    vertices[v.to].visited = 1;
                    q.push(v.to);
                }
            }
        }
    }
    
    void sparseDijkstra(int start) {
        distance[start] = 0;
        std::priority_queue<Node, vector<Node>, std::greater<Node>> minHeap;
        minHeap.push({start, distance[start]});

        while (!minHeap.empty()) {
            Node x = minHeap.top();
            minHeap.pop();
            int u = x.vertex, distance_u = x.distance;
            if (distance_u != distance[u]) continue;

            for (const Edge &edge : adjList[u]) {
                int v = edge.to;
                long long length = edge.weight;

                if (distance[u] + length < distance[v]) {
                    distance[v] = distance[u] + length;
                    trace[v] = u;
                    minHeap.push({v, distance[v]});
                }
            }
        }
    }
    
    long long shortestPath(int end) {
        return distance[end];
    }

    vector<int> tracePath(int start, int end) {
        if (end != start && trace[end] == -1) return vector<int>(0);
        vector<int> path;
        
        while (end != -1) {
            path.push_back(end);
            end = trace[end];
        }
        reverse(path.begin(), path.end());
        return path;
    }
    
    void clearVisited() {
        for (Vertex &u : vertices) u.visited = 0;
    }
};