#include <iostream>
#include <string>
#include <vector>

using namespace std;

struct step {
  char op;
  int num;
  step(char op, int num) : op(op), num(num) {}
};

int main() {
  int N;
  cin >> N;
  vector<step> steps;
  for (int i = 0; i < N; i++) {
    string opString;
    int num;
    cin >> opString >> num;
    steps.push_back(step(opString[0], num));
  }
  
  int total = 0;
  for (int i = 1; i <= 100; i++) {
    int cur = i;
    for (int j = 0; j < N; j++) {
      bool ok = true;
      switch(steps[j].op) {
        case 'A':
          cur += steps[j].num;
          break;
        case 'S':
          if (steps[j].num > cur) ok = false;
          cur -= steps[j].num;
          break;
        case 'M':
          cur *= steps[j].num;
          break;
        case 'D':
          if (cur % steps[j].num != 0) ok = false;
          cur /= steps[j].num;
          break;
      }
      if (!ok) {
        total++;
        break;
      }
    }
  }
  
  cout << total << endl;
}

