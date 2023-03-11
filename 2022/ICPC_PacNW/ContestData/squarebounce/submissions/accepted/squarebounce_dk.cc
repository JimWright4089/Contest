#include <cassert>
#include <iostream>
using namespace std;

template<typename T> constexpr T Gcd(const T& a, const T& b) { return b != 0 ? Gcd(b, a%b) : a < 0 ? -a : a; }

int main() {
  int64_t A, B, N;
  while (cin >> A >> B >> N) {
    N++;
    assert(Gcd(A, B) == 1);
    assert(B%2 || N < (A+B)/2);  // No corners.
    for (int64_t horiz = 0; ; horiz++)
    for (int64_t fudge = -1; fudge <= 1; fudge++) {
      assert(horiz < 2);
      int64_t den = horiz ? A : B;
      int64_t d = (N * den/(A+B) + fudge) * 2 + horiz, xc = d*B, yc = d*A+den;
      int64_t b = xc/(2*den) + yc/(2*den);
      if (d >= 0 && b == N) {
        int64_t x = xc%(4*den), y = yc%(4*den);
        x = (x < 2*den) ? x-den : 3*den-x; 
        y = (y < 2*den) ? y-den : 3*den-y; 
        cout << x/Gcd(x, den) << ' ' << den/Gcd(x, den) << ' ' << y/Gcd(y, den) << ' ' << den/Gcd(y, den) << endl;
        goto done;
      }
    }
done:;
  }
}
