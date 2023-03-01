#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>
#include <map>
#include <list>
#include <vector>
#include <ctype.h>

using namespace std;

const uint32_t MOD = 1000000009;

//   0000  0001  0010  0011  0100  0101  0110  0111
//   1000  1001  1010  1011  1100  1101  1110  1111

int bits[] = { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4};

long Count(uint32_t k, uint32_t b)
{
    uint32_t count = 0;
    uint32_t num = b / 30;
    if (0 != (b % 30))
    {
        num++;
    }

    for (uint32_t i = 0; i < num;i++)
    {
        uint32_t b1 = (b>30)?30:b;
        uint32_t b2 = pow(2,b1);

        for(uint32_t j=0;j<b2;j+=k)
        {
            uint32_t testNum = j%MOD;

            while (0 != testNum)
            {
                count += (bits[testNum % 0x10]);
                testNum /= 0x10;
            }
        }
    }

    return count;
}

int main() {
    long k = 0;
    long b = 0;
    std::cin >> k >> b;

    long n = Count(k,b);

    std::cout << n << std::endl;
}