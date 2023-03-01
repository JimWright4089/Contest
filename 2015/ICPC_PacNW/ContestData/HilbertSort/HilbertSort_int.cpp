#include <iostream>
#include <string>
#include <map>

#define X first
#define Y second
using namespace std;

int N, S;

int main()
{
    //ios::sync_with_stdio(0);
    while (cin >> N >> S && N)
    {
        map<long long, string> locs;
        for (int i = 0; i < N; ++i)
        {
            pair<int,int> p;
            string identifier;
            cin >> p.X >> p.Y;
            getline(cin, identifier);
            identifier = identifier.substr(1);
            
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
            locs[key] = identifier;
        }
        for (auto& loc : locs)
            cout << loc.second << endl;
    }
}
