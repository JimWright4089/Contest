#include <cassert>
#include <cmath>
#include <complex>
#include <iostream>
#include <tuple>
using namespace std;

#define MOD 1000000007

// Returned values (x,y,g) satisfy x*a+y*b = g.
// |x|+|y| is minimized, breaking ties with |x| <= |y|.
// If g>0, all other solutions are of the form (x+n*(b/g))*a + (y-n*(a/g))*b = g.
template<typename T> constexpr tuple<T,T,T> ExtendedGcd(T a, T b) {
	if( b == 0 ) {
    if (a == 0) return {0, 0, 0};
    if (a < 0) return {-1, 0, -a};
    return {1, 0, a};
	}
  auto [x, y, g] = ExtendedGcd(b, a%b);
  return {y, x-y*(a/b), g};
}

template<uint32_t Mod> struct ModInt {
  static_assert(Mod <= (1<<31), "Mod must be <= 2^31.");
  uint32_t v;
  constexpr ModInt() : v(0) {}
  constexpr ModInt(int64_t v) : v((uint32_t(v%Mod+Mod))%Mod) {}
  constexpr static ModInt __make(uint32_t v) { ModInt m; m.v = v; return m; }
  constexpr ModInt Inverse() const { return get<0>(ExtendedGcd(int32_t(v), int32_t(Mod))); }
  constexpr ModInt operator+(const ModInt& m) const { return __make((v+m.v)%Mod); }
  constexpr ModInt& operator+=(const ModInt& m) { v = (v+m.v)%Mod; return *this; }
  constexpr ModInt operator-(const ModInt& m) const { return __make((v+Mod-m.v)%Mod); }
  constexpr ModInt& operator-=(const ModInt& m) { v = (v+Mod-m.v)%Mod; return *this; }
  constexpr ModInt operator-() const { return __make((Mod-v)%Mod); }
  constexpr ModInt operator*(const ModInt& m) const { return __make(uint32_t(uint64_t(v)*m.v%Mod)); }
  constexpr ModInt& operator*=(const ModInt& m) { v = (uint64_t(v)*m.v)%Mod; return *this; }
  constexpr ModInt operator/(const ModInt& m) const { return *this * m.Inverse(); }
  constexpr ModInt& operator/=(const ModInt& m) { return *this *= m.Inverse(); }
  constexpr friend ModInt operator+(int64_t v, const ModInt& m) { return ModInt(v)+m; }
  constexpr friend ModInt operator-(int64_t v, const ModInt& m) { return ModInt(v)-m; }
  constexpr friend ModInt operator*(int64_t v, const ModInt& m) { return ModInt(v)*m; }
  constexpr friend ModInt operator/(int64_t v, const ModInt& m) { return ModInt(v)/m; }
  constexpr bool operator<(const ModInt& m) const { return v < m.v; }
  constexpr bool operator<=(const ModInt& m) const { return v <= m.v; }
  constexpr bool operator>(const ModInt& m) const { return v > m.v; }
  constexpr bool operator>=(const ModInt& m) const { return v >= m.v; }
  constexpr bool operator==(const ModInt& m) const { return v == m.v; }
  constexpr bool operator!=(const ModInt& m) const { return v != m.v; }
  constexpr friend bool operator==(int64_t v, const ModInt& m) { return ModInt(v)==m; }
  constexpr friend bool operator!=(int64_t v, const ModInt& m) { return ModInt(v)!=m; }
  constexpr friend ModInt abs(const ModInt& m) { return m; }
  constexpr explicit operator int() const { return v; }
  constexpr explicit operator bool() const { return v; }
  friend ostream& operator<<(ostream& out, const ModInt& m) { out << m.v; return out; }
};

template<typename T> T FastPow(T x, uint64_t n) {
  T ret(1);
  for (; n; n >>= 1, x *= x) if (n&1) ret *= x;
  return ret;
}

int main() {
  int64_t A, B, N;
  while (cin >> A >> B >> N) {
    assert(get<2>(ExtendedGcd(A, B)) == 1);
    complex<ModInt<MOD>> v(1-ModInt<MOD>(2)*B*B/(A*A+B*B), ModInt<MOD>(2)*A*B/(A*A+B*B));
    auto ret = FastPow(v, N+1);
    cout << -ret.real() << endl;
  }
}
