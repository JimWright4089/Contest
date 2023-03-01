#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>

using namespace std;

const int MAX_COUNTS = 1000;

double Goat(int x, int y, int x1, int y1, int x2, int y2)
{
    double num = 0;

    if ((x >= x1) && (x <= x2))
    {
        if (y <= y1)
        {
            num = (double)min(y1,y2) - y;
        }
        else
        {
            num = (double)y - max(y1,y2);
        }
    }
    else
    {
        if ((y >= y1) && (y <= y2))
        {
            if (x <= x1)
            {
                num = (double)min(x1, x2) - x;
            }
            else
            {
                num = (double)x - max(x1, x2);
            }
        }
        else
        {
            double d1 = hypot(x - x1, y - y1);
            double d2 = hypot(x - x1, y - y2);
            double d3 = hypot(x - x2, y - y1);
            double d4 = hypot(x - x2, y - y2);

            num = min(min(d1,d2),min(d3,d4));
        }
    }

    return num;
}


int main() {
    int x = 0;
    int y = 0;
    int x1 = 0;
    int y1 = 0;
    int x2 = 0;
    int y2 = 0;
    std::cin >> x >> y >> x1 >> y1 >> x2 >> y2;

    double n = Goat(x,y,x1,y1,x2,y2);

    std::cout << std::showpoint << std::fixed << std::setprecision(3) << n << std::endl;
}