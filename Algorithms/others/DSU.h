#include <vector>

struct DSU {
    std::vector<int> parent, sz;
    DSU(int n) {
        parent.resize(n + 1);
        sz.assign(n + 1, 1);
        for (int i = 0; i < n; i++) parent[i] = i;
    }

    int findSet(int v) {
        return v == parent[v] ? v : parent[v] = findSet(parent[v]);
    }

    void swap(int &a, int &b) {
        int t = a;
        a = b;
        b = t;
    }

    bool unionSet(int a, int b) {
        a = findSet(a);
        b = findSet(b);
        if (a != b) {
            if (sz[a] < sz[b]) swap(a, b);
            parent[b] = a;
            sz[a] += sz[b];
            return 1;
        }
        return 0;
    }
};