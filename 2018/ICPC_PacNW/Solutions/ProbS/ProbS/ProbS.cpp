#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>

using namespace std;
#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>

using namespace std;

const int MAX_COUNTS = 1000;

int main() {
    int lane[MAX_COUNTS];
    int num[MAX_COUNTS];

    for (int i = 0; i < MAX_COUNTS; i++)
    {
        lane[i] = 0;
        num[i] = 0;
    }

    int n = 0;
    int c = 0;
    std::cin >> n >> c;

    for (int i = 0; i < c; i++)
    {
        int s = 0;
        std::cin >> s;
        int min = 2000;
        int tl = 0;

        for (int j = 0; j < n; j++)
        {
            if (lane[j] < min)
            {
                tl = j;
                min = lane[j];
            }
        }

        for (int j = 0; j < n;j++)
        {
            lane[j] -= min;
        }
        num[i] = tl;
        lane[tl] = s;
    }

    for (int i = 0; i < c; i++)
    {
        std::cout << num[i] + 1;
        if(i<c-1)
        {
            std::cout << " ";
        }
    }
    std::cout <<  std::endl;
}
