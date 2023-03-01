#include <cassert>
#include <iomanip>
#include <iostream>
#include <vector>
using namespace std;

int main() {
  int N;
  while (cin >> N) {
    vector<int64_t> X(N), Y(N);
    for (int i = 0; i < N; i++) cin >> X[i] >> Y[i];
    int64_t ar = 0;
    for (int i = 1; i+1 < N; i++) ar += (Y[i+1]-Y[0])*(X[i]-X[0]) - (X[i+1]-X[0])*(Y[i]-Y[0]);
    assert(ar > 0);
    int64_t cur = 0;
    for (int i = 1; i+1 < N; i++) {
      int64_t next = cur + (Y[i+1]-Y[0])*(X[i]-X[0]) - (X[i+1]-X[0])*(Y[i]-Y[0]);
      if (2*next >= ar) {
        double f = (ar/2.0 - cur) / (next - cur);
        cout << setprecision(12) << (1-f)*X[i] + f*X[i+1] << ' ' << (1-f)*Y[i] + f*Y[i+1] << endl;
        break;
      }
      cur = next;
    }
  }
}
