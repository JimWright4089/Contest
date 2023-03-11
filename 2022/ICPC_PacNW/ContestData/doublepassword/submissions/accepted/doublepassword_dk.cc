#include <iostream>
#include <string>
using namespace std;

int main() {
  string A, B;
  while (cin >> A >> B) {
    int ret = 1;
    for (int i = 0; i < A.size(); i++) ret *= (A[i] == B[i]) ? 1 : 2;
    cout << ret << endl;
  }
}
