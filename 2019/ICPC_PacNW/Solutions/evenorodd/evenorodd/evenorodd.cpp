#include <iostream>
#include <iomanip>
#include <string>

using namespace std;

string EvenOrOdd(int n)
{
  if (1 == (n % 2))
  {
    return "Either";
  }

  int n2 = n / 2;

  if (1 == (n2 % 2))
  {
    return "Odd";
  }

  return "Even";
}


int main() {
  int n;
  std::cin >> n;
  string s = EvenOrOdd(n);
  std::cout << s << std::endl;
}
