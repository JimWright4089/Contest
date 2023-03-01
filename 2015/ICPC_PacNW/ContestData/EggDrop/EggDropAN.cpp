#include <iostream>
#include <string>

using namespace std;

int main() {
  int N, K;
  cin >> N >> K;
  int min = 2;
  int max = K - 1;
  for (int i = 0; i < N; i++) {
    int floor;
    string result;
    cin >> floor >> result;
    if (result == "SAFE") {
      if (min < floor + 1) min = floor + 1;
    } else {
      if (max > floor - 1) max = floor - 1;
    }
  }
  cout << min << " " << max << endl;
}

