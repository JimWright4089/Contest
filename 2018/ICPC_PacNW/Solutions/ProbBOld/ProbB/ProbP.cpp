#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>
//#include <map>

using namespace std;

//typedef map<int,int> InnerMap;
//typedef map<long, InnerMap> Finished;

//InnerMap gFinished;

void fill(long a, long b, long n)
{
/*
    int a1 = a;
    int b1 = b;

    if (a < b)
    {
        a1 = b;
        b1 = a;
    }

    InnerMap tmap = gFinished[a1];
    tmap[b1] = n;
*/
}

long find(long a, long b)
{
/*
    if (a < b)
    {
        InnerMap tmap = gFinished[a];
        if (0 != tmap[b])
        {
            return tmap[b];
        }
    }
    else
    {
        if (0 != gFinished[b][a])
        {
            InnerMap tmap = gFinished[b];
            return tmap[a];
        }
    }
*/
    return -1;
}


long GCD(long a, long b)
{
    int n = find(a,b);
    int a1 = a;
    int b1 = b;

    if(-1 == n)
    {
        while (b1 != 0)
        {
            long t = b1;
            b1 = a1%b1;
            a1 = t;
        }

        fill(a,b,a1);

        return a1;
    }

    return n;
}


long CoprimeIntegers(long a, long b, long c, long d)
{
    long num = 0;

    for (long i = a; i <= b; i++)
    {
        for (long j = c; j <= d; j++)
        {
            long n =  GCD(i,j);
            if (1 == n)
            {
                num++;
            }
        }
    }

    return num;
}


int main() {
    long a = 0;
    long b = 0;
    long c = 0;
    long d = 0;
    std::cin >> a >> b >> c >> d;

    long n = CoprimeIntegers(a,b,c,d);

    std::cout << n << std::endl;
}