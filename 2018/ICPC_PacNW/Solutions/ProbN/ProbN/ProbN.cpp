#include <iostream>
#include <iomanip>
#include <string>
#include <Math.h>

using namespace std;

int max(int a, int b)
{
    if (a > b)
    {
        return a;
    }
    return b;
}

int min(int a, int b)
{
    if (a < b)
    {
        return a;
    }
    return b;
}


int Exam(int numCorrect, string mine, string yours)
{
    int size = mine.size();
    int numInCorrect = size - numCorrect;
    int numSame = 0;
    int numNotSame = 0;

    for (int i = 0; i < size; i++)
    {
        if (mine.at(i) == yours.at(i))
        {
            numSame ++;
        }
        else
        {
            numNotSame++;
        }
        
    }

    return min(numNotSame+numCorrect,numSame + numInCorrect);
}

int main() {
    int numCorrect = 3;
    string mine = "ftfff";
    string yours = "tfttt";
    std::cin >> numCorrect >> mine >> yours;
    int maxAns = Exam(numCorrect,mine,yours);
    std::cout << maxAns << std::endl;
}