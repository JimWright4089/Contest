#include <iostream>
#include <iomanip>
#include <string>

using namespace std;

int main() 
{
  int n;
  int x;
  int y;
  std::cin >> n;
  std::cin >> x;
  std::cin >> y;

  double ratio = (double)y/(double)x;
  
  for (int i = 0; i < n; i++)
  {
    int a;
    std::cin >> a;
    double fltA = (double)a;
    fltA *= ratio;
    fltA += 0.5;
    a = (int)fltA;
    std::cout << a << std::endl;
  }
}
