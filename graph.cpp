#include <iostream>
#include <vector>
#include <queue>
#include <map>
#include "Graph.h"
using namespace std;

Graph g;
int vertex, edge, start;

void unweighted() {
    for (int i = 0; i < edge; i++) {
        int u, v; cin >> u >> v;
        g.addEdge(u, v);
    }

    g.printAllEdges();

    for (int u = 1; u <= vertex; u++) g.dfs(u);
    cout << '\n'; 
    g.clearVisited();

    for (int u = 1; u <= vertex; u++) g.bfs(u);
    cout << '\n';
    g.clearVisited();
}

void weighted() {
    cin >> vertex >> edge >> start;
    Graph wg(vertex);

    for (int i = 0; i < edge; i++) {
        int u, v;
        long long w; cin >> u >> v >> w;
        wg.addDirectedEdge(u, v, w);
    }

    // g.printAllWeightedEdges();
    // vector<int> path = wg.tracePath(start, vertex);
    // for (int edge : path) cout << edge << ' ';
    wg.sparseDijkstra(start);
    for (int i = 0; i < vertex; i++) {
        long long res = wg.shortestPath(i);
        cout << (res < g.INF ? res : -1) << ' ';
    }
}

int main() {
    // unweighted();

    weighted();
}
