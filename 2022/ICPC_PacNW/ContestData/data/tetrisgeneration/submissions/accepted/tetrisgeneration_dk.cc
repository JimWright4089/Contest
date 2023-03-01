#include <iostream>
#include <string>
using namespace std;

int main() {
  int T;
  for (cin >> T; T--;) {
    string S;
    cin >> S;
    int ret = 0;
    for (int shift = 0; shift < 7; shift++) {
      int i, b;
      for (i = 0, b = 0; i < S.size(); i++) {
        if (b&(1<<(S[i]-'A'))) break;
        b |= (1<<(S[i]-'A'));
        if (i%7 == shift) b = 0;
      }
      if (i == S.size()) ret = 1;
    }
    cout << ret << endl;
  }
}
