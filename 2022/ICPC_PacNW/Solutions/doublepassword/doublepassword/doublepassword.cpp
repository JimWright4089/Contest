#include <iostream>
#include <iomanip>
#include <string>
#include <math.h>

using namespace std;

int main() {
  int p1;
  int p2;
  std::cin >> p1;
  std::cin >> p2;

  int p11 = p1 % 10;
  int p21 = p2 % 10;
  int p12 = (p1 / 10) % 10;
  int p22 = (p2 / 10) % 10;
  int p13 = (p1 / 100) % 10;
  int p23 = (p2 / 100) % 10;
  int p14 = (p1 / 1000) % 10;
  int p24 = (p2 / 1000) % 10;

  int n=0;

  if (p11 != p21)
  {
    n++;
  }
  if (p12 != p22)
  {
    n++;
  }
  if (p13 != p23)
  {
    n++;
  }
  if (p14 != p24)
  {
    n++;
  }

  std::cout << pow(2,n) << std::endl;
}
