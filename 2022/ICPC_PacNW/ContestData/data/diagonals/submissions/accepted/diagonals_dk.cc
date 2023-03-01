#include <functional>
#include <iostream>
#include <string>
#include <vector>
using namespace std;

struct State {
  int N;
  vector<string> ret;
  vector<vector<int>> comp, ncon, ndis;

  State(const vector<string>& v) {
    N = v.size()-1;
    ret = vector<string>(N, string(N, '.'));
    comp = ncon = ndis = vector<vector<int>>(N+1, vector<int>(N+1, 100));
    for (int y = 0, v = 0; y <= N; y++) for (int x = 0; x <= N; x++) comp[y][x] = ++v;
    for (int y = 0; y <= N; y++)
    for (int x = 0; x <= N; x++) if (v[y][x] != '+') {
      ncon[y][x] = v[y][x]-'0';
      ndis[y][x] = 4 - ncon[y][x] - 2*(x==0||x==N) - 2*(y==0||y==N) + (x==0||x==N)*(y==0||y==N);
    }
  }

  void setComp(int x, int y, int c) {
    if (comp[y][x] == c) return;
    comp[y][x] = c;
    if (x > 0 && y > 0 && ret[y-1][x-1] == '\\') setComp(x-1, y-1, c);
    if (x > 0 && y < N && ret[y  ][x-1] == '/' ) setComp(x-1, y+1, c);
    if (x < N && y > 0 && ret[y-1][x  ] == '/' ) setComp(x+1, y-1, c);
    if (x < N && y < N && ret[y  ][x  ] == '\\') setComp(x+1, y+1, c);
  }

  bool testRet(int x, int y, char ch) {
    if ((ch == '\\' ? ncon : ndis)[y  ][x  ] == 0) return false;
    if ((ch == '/'  ? ncon : ndis)[y+1][x  ] == 0) return false;
    if ((ch == '/'  ? ncon : ndis)[y  ][x+1] == 0) return false;
    if ((ch == '\\' ? ncon : ndis)[y+1][x+1] == 0) return false;
    if (ch == '\\' && comp[y  ][x] == comp[y+1][x+1]) return false;
    if (ch == '/'  && comp[y+1][x] == comp[y  ][x+1]) return false;
    return true;
  }

  void setRet(int x, int y, char ch) {
    ret[y][x] = ch;
    (ch == '\\' ? ncon : ndis)[y  ][x  ]--;
    (ch == '/'  ? ncon : ndis)[y+1][x  ]--;
    (ch == '/'  ? ncon : ndis)[y  ][x+1]--;
    (ch == '\\' ? ncon : ndis)[y+1][x+1]--;
    if (ch == '\\') setComp(x, y  , comp[y+1][x+1]);
    if (ch == '/' ) setComp(x, y+1, comp[y  ][x+1]);
  }

  bool doLogic() {
    bool change = true;
    do {
      change = false;
      for (int y = 0; y < N; y++)
      for (int x = 0; x < N; x++) if (ret[y][x] == '.') {
        bool bs = testRet(x, y, '\\');
        bool fs = testRet(x, y, '/' );
        if (!bs && !fs) return false;
        if (!fs) { change = true; setRet(x, y, '\\'); }
        if (!bs) { change = true; setRet(x, y, '/' ); }
      }
    } while (change);
    return true;
  }

  bool rec() {
    if (!doLogic()) return false;
    for (int y = 0; y < N; y++)
    for (int x = 0; x < N; x++) if (ret[y][x] == '.') {
      State ost = *this;
      setRet(x, y, '\\');
      if (rec()) return true;
      *this = ost;
      setRet(x, y, '/' );
      return rec();
    }
    return true;
  }
};

int main() {
  int N;
  while (cin >> N) {
    vector<string> G(N+1);
    for (auto& s : G) cin >> s;
    State st(G);
    st.rec();
    for (auto const& s : st.ret) cout << s << endl;
  }
}
