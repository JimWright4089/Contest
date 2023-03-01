#include<cstdio>
#include<algorithm>
using namespace std;
struct wave
{
	int m, f, w;
	bool operator < (const wave &o) const { return m < o.m; }
};
int n;
wave a[302400];
int d[302400];
int main()
{
	int i, j;
	scanf("%d", &n);
	for (i = 1; i <= n; i++) scanf("%d%d%d", &a[i].m, &a[i].f, &a[i].w);
	sort(&a[1], &a[n + 1]);
	
    for (i = n; i >= 1; i--)
	{
		for (j = i + 1; j <= n && a[j].m < a[i].m + a[i].w; j++) ;
		d[i] = max(d[i + 1], d[j] + a[i].f);
	}
	
    printf("%d\n", d[1]);
	return 0;
}
