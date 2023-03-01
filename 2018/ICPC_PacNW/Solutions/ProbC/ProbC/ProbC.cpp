#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>
#include <map>
#include <list>
#include <vector>

using namespace std;

const int MOD = 998244353;

vector<long> gList;

int main() {
    long n = 0;
    long k = 0;
    std::cin >> n >> k;

    for (int i = 0; i < n; i++)
    {
        long x;
        std::cin >> x;
        gList.push_back(x);
    }

    std::sort(gList.begin(), gList.end());

    long dp[1001];
    long ndp[1001];

    for (int ii = 0; ii < k+1; ii++)
    {
        dp[ii]=0;
        ndp[ii] = 0;
    }

    dp[0] = 1;
    for (int i = 0; i < n;) 
    {
        int j = i + 1;
        while (j < n && gList[i] == gList[j])
        {
            j++;
        }

        int freq = j - i;

        ndp[0] += dp[0];
        for (int a = 1; a <= k; a++)
        {
            ndp[a] += dp[a] + (freq * dp[a - 1]);
            ndp[a] %= MOD;
        }
        for (int ii = 0; ii < k + 1; ii++)
        {
            dp[ii] = ndp[ii];
            ndp[ii] = 0;
        }
        i = j;
    }

    std::cout << dp[k] << std::endl;
}