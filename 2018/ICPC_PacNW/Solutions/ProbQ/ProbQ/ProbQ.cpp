#include <iostream>
#include <iomanip>
#include <string>

using namespace std;

const int MAX_RANKS = 13;

int index(char ch)
{
    if ('A' == ch)
    {
        return 0;
    }
    if ('T' == ch)
    {
        return 9;
    }
    if ('J' == ch)
    {
        return 10;
    }
    if ('Q' == ch)
    {
        return 11;
    }
    if ('K' == ch)
    {
        return 12;
    }

    return ch - '1';
}


int main() {
    string text = "";
    int size[MAX_RANKS];

    for (int i = 0; i < MAX_RANKS; i++)
    {
        size[i] = 0;
    }

    std::cin >> text;
    size[index(text.at(0))]++;
    std::cin >> text;
    size[index(text.at(0))]++;
    std::cin >> text;
    size[index(text.at(0))]++;
    std::cin >> text;
    size[index(text.at(0))]++;
    std::cin >> text;
    size[index(text.at(0))]++;

    int max = 0;

    for (int i = 0; i < MAX_RANKS; i++)
    {
        if (size[i] > max)
        {
            max = size[i];
        }
    }

    std::cout << max << std::endl;
}