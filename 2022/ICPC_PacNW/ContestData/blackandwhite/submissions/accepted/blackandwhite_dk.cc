#include <iomanip>
#include <iostream>
#include <vector>
using namespace std;

int main() {
  int N;
  while (cin >> N) {
    vector<double> P(N);
    for (auto& p : P) cin >> p;

    vector<double> dyn(1<<N);
    for (int b = 0; b < (1<<N); b++) {
      if (__builtin_popcount(b) <= 2) continue;
      double aw = 1.0, ab = 1.0, tot = 0.0;
      for (int i = 0; i < N; i++) if (b&(1<<i)) aw *= P[i];
      for (int i = 0; i < N; i++) if (b&(1<<i)) ab *= 1-P[i];
      for (int i = 0; i < N; i++) if (b&(1<<i)) {
        double p = aw/P[i] * (1-P[i]) + ab/(1-P[i]) * P[i];
        dyn[b] += p * dyn[b-(1<<i)];
        tot += p;
      }
      dyn[b] = (dyn[b]+1)/tot;
    }
    cout << setprecision(12) << dyn.back() << endl;
  }
}
