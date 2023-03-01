#include <iostream>
#include <iomanip>
#include <string>

using namespace std;

const int MAX_COUNTS = 1000;

int main() {
    int n = 0;
    int s = 0;
    std::cin >> n >> s;
    int maxT = -1;

    for (int i = 0; i < n; i++)
    {
        int ms = 0;
        std::cin >> ms;

        ms *= s;
        int ns = ms / 1000;
        if (0 != (ms % 1000))
        {
            ns++;
        }

        if (ns > maxT)
        {
            maxT = ns;
        }
    }

    std::cout << maxT << std::endl;
}