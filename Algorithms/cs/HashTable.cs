namespace FastHashTable
{
    public class HashTable
    {
        const int MOD = 1000367;
        const long MAXVAL = 1000000000000000000L;
        const long XOR = 727727727727727727L;
        const int MAXNODE = 2000000;
        private class Node
        {
            public long value;
            public Node next;
            public Node(long _value = 0)
            {
                value = _value;
                next = null;
            }    
        }
        private int nodeCount;
        private Node[] bucket, allNode;
        public HashTable() 
        {
            nodeCount = 0;
            bucket = new Node[MOD];
            for (int i = 0; i < MOD; i++) bucket[i] = null;
            allNode = new Node[MAXNODE];
        }
        private Node CreateNewNode(long val)
        {
            allNode[nodeCount] = new Node(val);
            return allNode[nodeCount++];
        }
        private int GetHashCode(long x)
        {
            x = (x + MAXVAL) ^ XOR;
            int res = (int)(x % MOD);
            return res < 0 ? res + MOD : res;
        }
        public bool Add(long x)
        {
            int h = GetHashCode(x);
            Node pointer = bucket[h];
            while (pointer != null)
            {
                if (pointer.value == x) return false;
                pointer = pointer.next;
            }
            Node newElement = CreateNewNode(x);
            newElement.next = bucket[h];
            bucket[h] = newElement;
            return true;
        }
    }
}