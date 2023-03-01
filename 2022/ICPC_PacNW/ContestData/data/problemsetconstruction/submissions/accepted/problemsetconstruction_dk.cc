#include <algorithm>
#include <iomanip>
#include <iostream>
#include <tuple>
#include <vector>
using namespace std;

int main() {
  int N, K, T;
  while (cin >> N >> K >> T) {
    vector<tuple<int, double, int>> P(N);
    for (int i = 0; i < N; i++) {
      int s;
      double p;
      cin >> p >> s;
      P[i] = {s, p, i};
    }
    sort(P.begin(), P.end());

    vector<double> ret(N);
    vector<vector<double>> dyn(K+1, vector<double>(T+1));
    dyn[0][0] = 1.0;
    for (int pi = 0; pi < P.size(); pi++) {
      auto [s, p, i] = P[pi];
      for (int k = K-1; k >= 0; k--)
      for (int t = 0; t+s <= T; t++) {
        ret[i] += dyn[k][t] * p;
        double inc = double(K-1-k)/(N-1-pi);
        dyn[k+1][t+s] += dyn[k][t] * inc *    p ;
        dyn[k+1][t  ] += dyn[k][t] * inc * (1-p);
        dyn[k][t] *= 1-inc;
      }
    }
    for (auto p : ret) cout << setprecision(15) << p << endl;
  }
}
