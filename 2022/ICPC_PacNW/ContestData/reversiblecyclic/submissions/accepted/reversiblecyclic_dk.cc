#include <iostream>
#include <string>
using namespace std;

#define MOD 1000000007
#define X 666

int main() {
  string S;
  while (cin >> S) {
    int64_t pw = 1;
    for (int i = 0; i < S.size()-1; i++) pw = (pw*X) % MOD;

    int64_t hash = 0;
    for (int i = 0; i < S.size()-1; i++) hash = (hash*X + S[i]) % MOD;
    int64_t hash1 = hash;
    int64_t hash2 = (hash*X + S.back() + MOD-pw*S[0]%MOD) % MOD;

    hash = 0;
    for (int i = S.size()-2; i >= 0; i--) hash = (hash*X + S[i]) % MOD;
    for (int i = S.size()-1; ; i--) {
      if (hash == hash1) hash1 = -1;
      if (hash == hash2) hash2 = -1;
      if (i == 0) break;
      hash = (hash*X + S[i] + MOD-pw*S[i-1]%MOD) % MOD;
    }
    cout << (hash1 == -1 && hash2 == -1) << endl;
  }
}
