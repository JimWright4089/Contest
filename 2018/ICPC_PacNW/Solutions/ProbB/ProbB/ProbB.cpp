#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>
#include <map>

using namespace std;

typedef map<int,long> InnerMap;
typedef map<long, InnerMap> Finished;

Finished gFinished;

void tfill(long a, long b, long n)
{
    int a1 = a;
    int b1 = b;

    if (a < b)
    {
        a1 = b;
        b1 = a;
    }

    InnerMap tmap = gFinished[a1];
    tmap[b1] = n;
}

long tfind(long a, long b)
{
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
    return -1;
}


bool isPrime(long n)
{
    return false;
    // Corner cases 
    if (n <= 1)
        return false;
    if (n <= 3)
        return true;

    // This is checked so that we can skip 
    // middle five numbers in below loop 
    if (n % 2 == 0 || n % 3 == 0)
        return false;

    for (int i = 5; i * i <= n; i = i + 6)
        if (n % i == 0 || n % (i + 2) == 0)
            return false;

    return true;
}

long GCD(long a, long b)
{
    int a1 = a;
    int b1 = b;

    while (b1 != 0)
    {
        long t = b1;
        b1 = a1 % b1;
        a1 = t;
    }

    return a1;
}

int p[10000001];
bool bad[10000001];

long CoprimeIntegers(long a, long b, long c, long d)
{
    long num = 0;

    for (long i = a; i <= b; i++)
    {
        for (long j = c; j <= d; j++)
        {
            long n = tfind(i, j);
            if (-1 == n)
            {
                n = GCD(i, j);
                if (1 == n)
                {
                    tfill(i, j, 1);
                    num++;
                }
                else
                {
                    tfill(i, j, 0);
                }
            }
            else
            {
                num += n;
            }
        }
    }

    return num;
}

long CoprimeIntegersx(long a, long b, long c, long d)
{
    long num = 0;

    for (long i = a; i <= b; i++)
    {
        for (long j = c; j <= d; j++)
        {
            long n = tfind(i, j);
            if(-1 == n)
            {
                n = GCD(i, j);
                if (1 == n)
                {
                    tfill(i,j,1);
                    num++;
                }
                else
                {
                    tfill(i,j,0);
                }
            }
            else
            {
                num += n;
            }
        }
    }

    return num;
}

long long solve(long long a, long long b)
{
    long long ret = (long long)a * (long long)b;
    for (long long i = 2; i <= min(a, b); i++) 
    {
        if (false == bad[i])
        {
            long long val = (a / i) * (b / i);
            if (p[i] % 2 != 0)
            {
                ret -= val;
            }
            else
            {
                ret += val;
            }
        }
    }
    return ret;

}

void init(long maxv)
{
    for (long i = 2; i <= maxv; i++) 
    {
        if (p[i] == 0)
        {
            for (long j = i; j <= maxv; j += i) 
            {
                p[j]++;
            }
            long long i2 = (long long)i* (long long)i;
            if (i2 <= maxv)
            {
                for (long long j = i2; j <= maxv; j += i2) 
                {
                    bad[j] = true;
                }
            }
        }
    }
}


int main() {
    long a = 0;
    long b = 0;
    long c = 0;
    long d = 0;
    std::cin >> a >> b >> c >> d;

    init(min(b,d));

//    long n = CoprimeIntegers(a, b, c, d);
    long long n = solve(b, d) - solve(a - 1, d) - solve(b, c - 1) + solve(a - 1, c - 1);

    std::cout << n << std::endl;
}