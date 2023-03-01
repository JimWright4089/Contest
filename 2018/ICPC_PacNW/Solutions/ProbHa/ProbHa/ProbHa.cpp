#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>
#include <map>
#include <list>
#include <vector>

using namespace std;

const long MAX_NUM = 1000001;
bool isPrimeA[MAX_NUM];

/*
        isPrime[0] = isPrime[1] = false;
        for( int i=2; i<isPrime.length; i++ ) if( isPrime[i] )
        {
            for( int j=i+i; j<isPrime.length; j+=i ) isPrime[j] = false;
        }
*/


void initIt(void)
{
    for (int i = 0; i < MAX_NUM; i++)
    {
        isPrimeA[i] = true;
    }
    isPrimeA[0] = isPrimeA[1] = false;
    for (int i = 2; i < MAX_NUM; i++)
    {
        if (true == isPrimeA[i])
        {
            for (int j = i + i; j < MAX_NUM; j += i)
            {
                isPrimeA[j] = false;
            }
        }
    }
}

/*
while (n > 3)
{
    int i;
    for (i = 2; i < isPrime.length; i++) if (isPrime[i] && isPrime[n - i]) break;
    n -= i + i;
    ++count;
}
*/

long doIt(long n)
{
    int count = 0;
    while (n > 3)
    {
        int i;
        for (i = 2; i < MAX_NUM; i++)
        {
            if (isPrimeA[i] && isPrimeA[n - i])
            {
//                printf("%7d %7d %7d %7d\n", n, i, n - i, count);
                break;
            }
        }
        n -= i + i;
        ++count;
    }
    return count;
}

int main(void)
{
    long n = 0;
    long x = 0;
    std::cin >> x;

    initIt();

    n = doIt(x);

    std::cout << n << std::endl;
    return n;
}


