#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main() {
  int N;
  string V, S, Q;
  while (cin >> V >> S >> N) {
    S = '.'+S;
    vector<vector<int32_t>> next(S.size(), vector<int32_t>(26, -1));
    int len = 1, vb = 0;
    for (auto ch : V) vb |= (1<<(ch-'a'));
    for (int i = 1, b = 0; i < S.size(); i++) {
      b |= (1<<(S[i]-'a'));
      if (b == vb) { b = 0; len++; }
      for (int j = i-1; j >= 0; j--) {
        next[j][S[i]-'a'] = i;
        if (S[j] == S[i]) break;
      }
    }
    for (int i = 0; i < N; i++) {
      cin >> Q;
      int pos = 0;
      for (auto ch : Q) {
        pos = next[pos][ch-'a'];
        if (pos == -1) break;
      }
      cout << (pos == -1 && Q.size() == len) << endl;
    }
  }
}
