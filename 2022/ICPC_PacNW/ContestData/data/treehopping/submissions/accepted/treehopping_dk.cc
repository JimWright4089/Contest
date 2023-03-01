#include <functional>
#include <iostream>
#include <vector>
using namespace std;

int main() {
  int T, N, A, B;
  for (cin >> T; T--;) {
    cin >> N;
    vector<vector<int>> E(N+1);
    for (int i = 0; i < N-1; i++) {
      cin >> A >> B;
      E[A].push_back(B);
      E[B].push_back(A);
    }
    vector<int> P(N);
    for (auto& p : P) cin >> p;

    vector<int> par(N+1), depth(N+1);
    function<void(int,int,int)> rec = [&](int x, int prev, int d) {
      par[x] = prev;
      depth[x] = d;
      for (auto y : E[x]) if (y != prev) rec(y, x, d+1);
    };
    rec(1, -1, 0);

    int ret = 1;
    for (int i = 0; i+1 < P.size(); i++) {
      int x = P[i], y = P[i+1], d = 0;
      while (d <= 3 && depth[x] > depth[y]) { x = par[x]; d++; }
      while (d <= 3 && depth[y] > depth[x]) { y = par[y]; d++; }
      while (d <= 3 && depth[x] && x != y) { x = par[x]; y = par[y]; d += 2; }
      if (d > 3) ret = 0;
    }
    cout << ret << endl;
  }
}
