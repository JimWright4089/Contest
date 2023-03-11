#include <algorithm>
#include <functional>
#include <iostream>
#include <set>
#include <vector>
using namespace std;

int main() {
  int N, K;
  while (cin >> N >> K) {
    vector<int> R(1<<N);
    for (auto& r : R) cin >> r;
    sort(R.begin(), R.end(), greater<int>());

    int ret = 0;
    for (int i = 0; i < N; i++) {
      set<int> opp;
      for (int j = (1<<i); j < (2<<i); j++) opp.insert(R[j]);
      for (int j = 0; j < (1<<i); j++) {
        auto it = opp.lower_bound(R[j] - K);
        if (it != opp.end()) {
          ret++;
          opp.erase(it);
        }
      }
    }
    cout << ret << endl;
  }
}
