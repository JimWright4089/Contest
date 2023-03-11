#include <algorithm>
#include <functional>
#include <iostream>
#include <vector>
using namespace std;

int main() {
  int N;
  while (cin >> N) {
    vector<int> A(N);
    for (auto& a : A) cin >> a;

    vector<vector<int>> triple(N, vector<int>(N));
    for (int i = 0; i < N; i++)
    for (int j = 0; j < N; j++)
    for (int k = 0; k < N; k++) {
      if ((A[i]^A[j]) == A[k]) triple[i][j] |= (1<<k);
    }

    vector<int> cur;
    int ret = N;
    function<void(int,int)> rec = [&](int i, int b) {
      while (b&(1<<i)) i++;
      if (i >= N) {
        ret = min<int>(ret, N-cur.size());
        return;
      }
      rec(i+1, b);
      for (auto j : cur) b |= triple[i][j];
      cur.push_back(i);
      rec(i+1, b);
      cur.pop_back();
    };
    rec(0, 0);
    cout << ret << endl;
  }
}
