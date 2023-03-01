#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>

using namespace std;

int main() {
    long m = 0;
    std::cin >> m;
    long x = m + 1;
    long n = x + 1;
    long long sum_left = m;
    long long sum_right = n;

    while (sum_left != sum_right) 
    {
        if (sum_right < sum_left) 
        {
            n++;
            sum_right += n;
        }
        else 
        { // sum_left < sum_right
            sum_left += x;
            x++;
            sum_right -= x;
        }
    }

    std::cout << m << " " << x << " "<< n << std::endl;
}