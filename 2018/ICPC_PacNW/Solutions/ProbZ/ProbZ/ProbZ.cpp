#include <iostream>
#include <iomanip>
#include <string>

using namespace std;

int main() {
    int a1 = 0;
    int r1 = 0;
    int p1 = 0;
    int p2 = 0;
    std::cin >> a1 >> p1 >> r1 >> p2;

    double ap1 = (double)p1 / (double)a1;
    double ap = (double)r1*(double)r1*3.1415927;

    double ap2 = (double)p2 / ap;

    if (ap1 < ap2)
    {
        std::cout << "Slice of pizza" << std::endl;
    }
    else
    {
        std::cout << "Whole pizza" << std::endl;
    }
}