#include <cassert>
#include <iostream>
using namespace std;

int main() {
  int N, X, Y, A;
  while (cin >> N >> X >> Y) {
    for (int i = 0; i < N; i++) {
      cin >> A;
      assert(A*Y%X == 0);
      cout << A*Y/X << endl;
    }
  }
}
