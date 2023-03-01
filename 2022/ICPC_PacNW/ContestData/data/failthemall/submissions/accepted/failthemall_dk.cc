#include <functional>
#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main() {
  int N, K;
  while (cin >> N >> K) {
    vector<string> S(N);
    for (auto& s : S) cin >> s;

    string ret(K, '.');
    function<bool(int,char)> setRet = [&](int x, char ch) {
      if (ret[x] != '.') return ret[x] == ch;
      ret[x] = ch;
      for (auto& s : S) if (s[x] == ch) {
        for (int i = 0; i < s.size(); i++) if (i != x && s[i] != 'X') {
          if (!setRet(i, s[i]^'F'^'T')) return false;
        }
      }
      return true;
    };
    for (int i = 0; i < K; i++) if (ret[i] == '.') {
      for (int j = 0; ; j++) {
        if (j == 2) goto fail;
        string oret = ret;
        if (setRet(i, "FT"[j])) break;
        ret = oret;
      }
    }

    cout << ret << endl;
    continue;
fail:
    cout << -1 << endl;
  }
}
