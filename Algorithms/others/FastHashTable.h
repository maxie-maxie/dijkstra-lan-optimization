#include <vector>

#define ll long long

const int HTMOD = (int)1e6 + 367;
const ll MAX_VAL = (ll)1e18 + 727;
const ll WYSI = 727727727727727727LL;
const int MAX_ELE = 7272727;

struct Element {
    ll val;
    Element* nxt;

    Element(ll _val = 0) {
        val = _val;
        nxt = nullptr;
    }
};

int eleCnt;
Element allEle[MAX_ELE];
Element* createNewEle(ll val) {
    allEle[eleCnt] = Element(val);
    return &allEle[eleCnt++];
}

struct HashTable {
    int getHash(ll x) {
        x = (x + MAX_VAL) ^ WYSI;
        int res = x % HTMOD;
        return res < 0 ? res + HTMOD : res;
    }

    std::vector<Element*> bucket;

    HashTable() {
        bucket.assign(HTMOD, nullptr);
    }

    bool find(ll x) {
        int h = getHash(x);
        Element *ptr = bucket[h];
        while (ptr != nullptr) {
            if (ptr->val == x) return 1;
            ptr = ptr->nxt;
        }
        return 0;
    }

    bool insert(ll x) {
        int h = getHash(x);
        if (find(x)) return 0;
        Element* newEle = createNewEle(x);
        newEle->nxt = bucket[h];
        bucket[h] = newEle;
        return 1;
    }
};