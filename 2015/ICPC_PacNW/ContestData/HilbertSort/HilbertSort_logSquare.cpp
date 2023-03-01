#include <iostream>
#include <string>
#include <vector>
#include <utility>
#include <algorithm>

#define X first
#define Y second
using namespace std;
typedef pair<int,int> pii;

int N, S;

long long key(pii p)
{
    p = make_pair(2*p.X - S, 2*p.Y - S);
    long long key = 0;
    for (int i = 0; i < 30; ++i)
    {
        key *= 4;
        if (p.X < 0)
        {
            if (p.Y < 0)
            {
                p = make_pair(2*p.Y + S, 2*p.X + S);
            }
            else
            {
                key += 1;
                p = make_pair(2*p.X + S, 2*p.Y - S);
            }
        }
        else
        {
            if (p.Y >= 0)
            {
                key += 2;
                p = make_pair(2*p.X - S, 2*p.Y - S);
            }
            else
            {
                key += 3;
                p = make_pair(-(2*p.Y + S), -(2*p.X - S));
            }
        }
    }
    return key;
}

bool compLocs(const pair<pii,string>& a, const pair<pii,string>& b)
{
    return key(a.first) < key(b.first);
}

int main()
{
    //ios::sync_with_stdio(0);
    while (cin >> N >> S && N)
    {
        vector<pair<pii,string> > locs(N);
        for (int i = 0; i < N; ++i)
            cin >> locs[i].first.first >> locs[i].first.second >> locs[i].second;
        sort(locs.begin(), locs.end(), compLocs);
        for (auto& loc : locs)
            cout << loc.second << endl;
    }
}
