#include <algorithm>
#include <iostream>
#include <tuple>
#include <vector>
using namespace std;

int main() {
  int N, K;
  while (cin >> N >> K) {
    vector<vector<int>> G(N, vector<int>(N));
    vector<tuple<int, int, int>> v;
    for (int y = 0; y < N; y++)
    for (int x = 0; x < N; x++) {
      cin >> G[y][x];
      v.emplace_back(G[y][x], x, y);
    }
    sort(v.begin(), v.end());

    vector<int64_t> rc(N), cc(N);
    int64_t ret = 1e18;
    for (int n = 1, i = 0; n <= K; n++) {
      auto orc = rc, occ = cc;
      rc = cc = vector<int64_t>(N, 1e18);
      for (; i < v.size(); i++) {
        auto [s, x, y] = v[i];
        if (s != n) break;
        int64_t d = min(orc[y], occ[x]);
        if (s == K) ret = min(ret, d);
        for (int x2 = 0; x2 < N; x2++) cc[x2] = min(cc[x2], d + (x2-x)*(x2-x));
        for (int y2 = 0; y2 < N; y2++) rc[y2] = min(rc[y2], d + (y2-y)*(y2-y));
      }
    }
    cout << (ret == 1e18 ? -1 : ret) << endl;
  }
}
