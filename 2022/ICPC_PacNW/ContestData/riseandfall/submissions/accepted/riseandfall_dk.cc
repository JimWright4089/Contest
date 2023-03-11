#include <algorithm>
#include <iostream>
#include <string>
using namespace std;

int main() {
  int T;
  for (cin >> T; T--;) {
    string S;
    cin >> S;
    int i, j;
    for (i = 1; i < S.size(); i++) if (S[i] < S[i-1]) break;
    char dig = S[i];
    for (; i < S.size(); i++) {
      if (S[i] > dig) break;
      S[i] = dig = min(dig, S[i]);
    }
    for (; i < S.size(); i++) S[i] = dig;
    cout << S << endl;
  }
}
