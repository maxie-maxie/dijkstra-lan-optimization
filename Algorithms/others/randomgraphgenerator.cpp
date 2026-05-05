#include <iostream>
#include <random>
#include <algorithm>
#include <vector>
#include "DSU.h"
#include "FastHashTable.h"
using std::cin;
using std::cout;
using std::vector;
using std::mt19937;

struct Edge {
    int u, v;
    int w;
};

struct Node {
    int v;
    int w;
};

mt19937 rng(1337);
int randInt(int l, int r) {
    return std::uniform_int_distribution<int>(l, r)(rng);
}

vector<Edge> graphGen(int v, int e) {
    DSU g(v);
    HashTable ht;
    vector<Edge> el;
    int cnt = 0;
    if (e >= v - 1) {
        while (cnt < v - 1) {
            int a = randInt(1, v), b = randInt(1, v);
            if (a != b && g.unionSet(a, b)) {
                int weight = randInt(1, 1024);
                el.push_back({a, b, weight});
                cnt++;
            }
        }

        for (const Edge &edge : el) {
            ll key = 1LL * std::min(edge.u, edge.v) * (v + 1) + std::max(edge.u, edge.v);
            ht.insert(key);
        }
    }

    long long max_e = 1LL * v * (v - 1) / 2;
    if (e > max_e) e = (int)max_e; 
    
    while (cnt < e) {
        int a = randInt(1, v), b = randInt(1, v);
        if (a == b) continue;
        ll key = 1LL * std::min(a, b) * (v + 1) + std::max(a, b);
        if (ht.insert(key)) {
            int weight = randInt(1, 1024);
            el.push_back({a, b, weight});
            cnt++;
        }
    }
    return el;
}

signed main() {
    std::ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    int n, m; cin >> n >> m;
    vector<Edge> graph = graphGen(n, m);

    vector<Node> adj[n + 1];
    for (const Edge &e : graph) {
        adj[e.u].push_back({e.v, e.w});
        adj[e.v].push_back({e.u, e.w});
    }
    for (int u = 1; u <= n; u++) {
        cout << u << ": ";
        for (const Node &v : adj[u]) cout << v.v << ',' << v.w << ' ';
        cout << '\n';
    }
}