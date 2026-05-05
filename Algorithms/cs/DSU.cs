namespace DisjointSetUnion
{
    public struct DSU
    {
        private int[] parent, size;
        public DSU(int n = 0)
        {
            parent = new int[n + 1];
            size = new int[n + 1];
            for (int i = 0; i <= n; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }
        private int FindSet(int v)
        {
            return v == parent[v] ? v : parent[v] = FindSet(parent[v]);
        }
        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public bool UnionSet(int a, int b)
        {
            a = FindSet(a);
            b = FindSet(b);
            if (a != b)
            {
                if (size[a] < size[b]) Swap(ref a, ref b);
                parent[b] = a;
                size[b] += size[a];
                return true;
            }
            return false;
        }
    }
}
