#include <iostream>
#include <iomanip>

using namespace std;

const int MAXE = 10000000;
int E, Esucc, Efail;
long long lifts[MAXE+1];

int main()
{
    //ios::sync_with_stdio(0);
    while (cin >> E >> Esucc >> Efail && E)
    {
        for (int i = 1; i <= E; ++i)
        {
            lifts[i] = 1;
            if (i > Esucc)
                lifts[i] += lifts[i-Esucc];
            if (i > Efail)
                lifts[i] += lifts[i-Efail];
            if (lifts[i] > 1e18)
                lifts[i] = 1e18;
        }
        long double ans = min(200.0/lifts[E], 225.0/(lifts[E]+1));
        cout << fixed << setprecision(6) << ans << endl;
        int residualDigit = int(10000000*ans) % 10;
        if (residualDigit == 4 || residualDigit == 5)
            cerr << "WARNING: " << residualDigit << " in the first unseen place" << endl;
    }
}
