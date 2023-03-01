#include <iostream>
#include <iomanip>
#include <string>

using namespace std;

const int MAX_COUNTS = 1000;

int main() {

    int counts[MAX_COUNTS];

    for (int i = 0; i < MAX_COUNTS; i++)
    {
        counts[i] = 0;
    }

    int n = 0;
    std::cin >> n;

    for (int i = 0; i < n; i++)
    {
        int s=0;
        int e=0;

        std::cin >> s >> e;

        for (int j = s; j <= e; j++)
        {
            counts[j]++;
        }
    }

    int max = -1;

    for (int i = n - 1; i >= 0; i--)
    {
        if (i == counts[i])
        {
            max = i;
            break;
        }
    }

    std::cout << max << std::endl;
}