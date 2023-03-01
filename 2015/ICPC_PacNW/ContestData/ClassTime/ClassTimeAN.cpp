#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

struct name {
  string first, last;
  name(string first, string last) : first(first), last(last) {}
};

bool cmp(const name &a, const name &b) {
  if (a.last == b.last) return a.first < b.first;
  return a.last < b.last;
}

int main() {
  int N;
  cin >> N;
  vector<name> names;
  for (int i = 0; i < N; i++) {
    string first, last;
    cin >> first >> last;
    names.push_back(name(first, last));
  }
  sort(names.begin(), names.end(), &cmp);
  for (int i = 0; i < N; i++) {
    cout << names[i].first << " " << names[i].last << endl;
  }
}

