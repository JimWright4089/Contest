#include <algorithm>
#include <cmath>
#include <functional>
#include <iomanip>
#include <iostream>
#include <vector>
using namespace std;

int N;
vector<double> P;
vector<int> S;

double walk(double p, int up, int down) {
  if (p == 0.5) return double(down) / (up+down);
  return (1-pow((1-p)/p, down)) / (1-pow((1-p)/p, up+down));
}

int main() {
  while (cin >> N) {
    P.resize(N);
    for (auto& p : P) cin >> p;
    S.resize(N);
    for (auto& s : S) cin >> s;
    S.insert(S.begin(), 0);

    vector<double> memo(1<<N, -1.0);
    function<double(int,int)> doit = [&](int r, int b) {
      if (b == 0) return 1.0;
      double& ret = memo[b];
      if (ret >= 0.0) return ret;
      ret = 0.0;
      for (int i = 0; i < N; i++) if (b&(1<<i)) {
        double p = doit(r-1, b-(1<<i));
        ret = max(ret,    p  * walk(P[i], S[r]-S[r-1], S[r]+S[r-1]) +
                       (1-p) * walk(P[i], S[r]+S[r-1], S[r]-S[r-1]));
      }
      return ret;
    };
    cout << setprecision(12) << doit(N, (1<<N)-1) << endl;
  }
}
