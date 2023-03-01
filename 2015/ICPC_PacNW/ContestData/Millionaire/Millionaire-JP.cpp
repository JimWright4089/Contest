#include<cstdio>
#include<cmath>
int n;
double W;
struct quiz { double p, v; bool safeflag; } a[100100];
double P[100100];
int main()
{
	scanf("%d%lf", &n, &W);
	P[0] = 1.0;
	for (int i = 1; i <= n; i++)
	{
		char buf[16];
		scanf("%s%lf%lf", buf, &a[i].p, &a[i].v);
		a[i].safeflag = buf[0] == 's';
		P[i] = P[i - 1] * a[i].p;
	}
	double hh = 0.0, hmax = 0.0;
	int lsti = 0;
	for (int i = 1; i <= n; i++)
	{
		double h = hh + (P[lsti] - P[i])*log(1.0 + a[lsti].v / W) + P[i] * log(1.0 + a[i].v / W);
		if (hmax < h) hmax = h;
		if (a[i].safeflag)
		{
			hh += (P[lsti] - P[i])*log(1.0 + a[lsti].v / W);
			lsti = i;
		}
	}
	printf("$%.2lf\n", W*(exp(hmax) - 1.0));
	return 0;
}

